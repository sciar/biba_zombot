using UnityEngine;
using System.Collections;
using BibaFramework.BibaGame;
using UnityEngine.UI;

[RequireComponent (typeof (BibaCanvasGroup))]

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public AudioServices audioServices;
	public Animator mainGameAnimator;
	public CanvasGroup textGroup; // Text with Jail on it that fades when you have someone in jail
	public GameObject[] touchMarkerObjects;
	public ChainLink[] chainLinks;
	public Camera mainCam;
	public ParticleSystem explosionEmmiter;
	public float explosiveForce = 1000f;

	private Vector3 originalPivotPosition;
	private int numberOfRobbers = 5;

    // New Version
    public UnityEngine.AudioClip gameMusic;
    private UnityEngine.AudioClip originalBGMusic;
    public int copScore;
    private int copScoreWin = 10;//How many points it takes to win for cops
    public Text copScoreText;
	public int robberScore;
    private int robberScoreWin = 15;//How many points it takes to win for robbers
    public Text robberScoreText;
    public bool win; // Check to make sure you can only win once
	private float[] arrestTimer;
	private float arrestTimerMax; // How long people will be in jail for
    public int prisonerReleaseCharge; // How many people have earned being able to let go
    private bool lootSide = false;
    public GameObject treasurePile;
    [HideInInspector]
    public int lootPileCount = 0; // Keeps count of how many loot tokens we've got
    private float lootPileBuffer;
    public float gameTime; // How long we've been on this screen this round

    //Jailbreak Effect
    public GameObject jailbreakEffect;

    // Z Axis Fix on Screen Touch Updates
    private float defaultZ;

    public string victor; // Called by other scripts to check who won (Probably only the the win screen)
    public BibaCanvasGroup bibaCanvasGroup;

    public Animator bibaRootAnimator; // Used to check for the variables that are active

    public void SetNumberOfRobbers(int number) // This is set on the Select Robbers screen (Currently unused)
    {
		numberOfRobbers = number;
	}

	void Awake() {

        //Singletons woo go team
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
            
        originalPivotPosition = chainLinks[0].pivot.transform.position;
	}

    void OnEnable()
    {
        bibaCanvasGroup.onCanvasGroupEnterSuscribers += ResetVariables;
    }

    void OnDisable()
    {
        bibaCanvasGroup.onCanvasGroupEnterSuscribers -= ResetVariables;
        AudioManager.Instance.StartBGMusic();
    }

    void ResetVariables() // Every time we come back to this screen we reset the necessary variables
    {
        gameTime = 0; //Reset the gametime (Used by treasure to easy reset)

        originalBGMusic = AudioManager.Instance.bgMusic.clip;
        AudioManager.Instance.bgMusic.Stop();
        AudioManager.Instance.bgMusic.clip = gameMusic;
        AudioManager.Instance.bgMusic.Play();

        win = false;
        copScore = 0;
        robberScore = 0;
        defaultZ = touchMarkerObjects[0].transform.position.z; // Sets up the default Z position for the touch marker objects

        foreach (GameObject g in touchMarkerObjects)
        {
            g.SetActive(false);
        }
            
        textGroup.FadeAlphaTo (1f, 0.25f);

        jailbreakEffect.SetActive(false); // Just a safety check to make sure this effect is turned off
	    
        //Reset the loot count so we don't animate the wrong thing
        lootPileCount = 0;

        bibaRootAnimator = GameObject.Find("MenuStateMachine").GetComponent<Animator>();

        if (bibaRootAnimator.GetBool("ShowTagScan") == true) // If they've selected yes go to the AR moments
            mainGameAnimator.SetBool("BibaPlayground", true);
    }

	void Update() { // UPDATE!?!? I WONDER WHAT THIS DOES? Elf magic? Definitely elf magic.

        if (lootPileBuffer > 0) // Sets a small buffer to protect against double taps
            lootPileBuffer -= Time.deltaTime;
        
        gameTime += Time.deltaTime; // Increment Gametime

        // Arrest Touches
        int touchCount = Input.touchCount;

        if (touchCount > 0)
        {
            for (int i = 0; i < touchCount; i++)
            {
                lootSide = false; // Reset which side we're on for each touch
                if (Input.GetTouch(i).position.x < (Screen.width / 2))
                {
                    lootSide = true; // If our touch is on the left side of the screen it's Loot not an arrest 
                }

                Touch touch = Input.GetTouch(i);
                TouchPhase phase = touch.phase;

                switch (phase)
                {
                    case TouchPhase.Began: // This triggers when a player first touches the screen
                        //Debug.LogError("New touch detected " + touch.fingerId);
                        if (!lootSide)
                            StartTouch(touch.fingerId);
                        else if (lootSide)
                            LootDrop(mainCam.ScreenToWorldPoint(Input.GetTouch(i).position));
                        break;
                    case TouchPhase.Ended: // This triggers when a player lets go of their touch
                        //Debug.LogError("Touch " + touch.fingerId + " Has Ended");
                        if (touchMarkerObjects[touch.fingerId].activeSelf)
                        {// First we check if the object is still active and if yes we add points for letting go early
                            JailBreak();
                        }
                        StopTouch(touch.fingerId);
                        break;
                    case TouchPhase.Canceled: // This case should only trigger if someone tries to palm the full screen or too many touches are detected (aka 9+)
                        Debug.LogError("Touch index " + touch.fingerId + " cancelled");
                        break;
                }
                if (!lootSide) // As long as we aren't on the same side as the loot page update the positions of everything
                { 
                    Vector3 temporaryPosition = mainCam.ScreenToWorldPoint(Input.GetTouch(i).position);
                    temporaryPosition.z = defaultZ;
                    touchMarkerObjects[touch.fingerId].transform.position = temporaryPosition;
                }
            }
        }
        else
        {
            textGroup.FadeAlphaTo (1f, 0.25f); // Fade the text back in since we have zero touches
        }

        // Score Tracking text updates
        copScoreText.text = copScore.ToString() + "/" + copScoreWin.ToString();
        robberScoreText.text = robberScore.ToString() + "/" + robberScoreWin.ToString();

        if (copScore >= copScoreWin && !win) { gameOver("cops"); win = true; }; // Cop victory condition
        if (robberScore >= robberScoreWin && !win) { gameOver("robbers"); win = true; }; // Robber victory condition
	}

	void StartTouch(int fingerTracker)
	{ // On touching the screen we spawn a circle
		copScore++; // Cops get one point for an arrest
        ResetPivotRigidbodies(fingerTracker); // Reset the chain location
        textGroup.FadeAlphaTo (0f, 0.25f); // This is the canvas with the text "Jail" in it and we are fading it out
        touchMarkerObjects[fingerTracker].SetActive(true);
        touchMarkerObjects[fingerTracker].GetComponent<JailTimer>().touchNumber = fingerTracker; // Sends the finger track # to the script
        AudioManager.Instance.PlaySFX(AudioManager.Instance.chainSFX); // Play chain sound when you get an arrest
	}
    void StopTouch(int fingerTracker)
    {
        touchMarkerObjects[fingerTracker].SetActive(false);
    }

    public void ExplodeTouch(int touchNumber) // Cool explosion when your timer runs out
	{
        var c = chainLinks[touchNumber];

        Rigidbody2D r = c.pivot.GetComponent<Rigidbody2D>();
        r.isKinematic = false;
        foreach(Rigidbody2D chainr in c.chainBodies)
        {
            chainr.GetComponent<HingeJoint2D>().enabled = false;
            chainr.AddForce(new Vector2(Random.Range(-1f, 1f),
            Random.Range(-1f, 1f))*explosiveForce*0.3f,ForceMode2D.Force);
        }
        r.AddForce(new Vector2(Random.Range(-1f, 1f),Random.Range(-1f, 1f))*explosiveForce,ForceMode2D.Force);
        explosionEmmiter.Emit(40); 

        // Tells the player they're free with some text at their finger spot
        GameObject freeMessage = (GameObject)Instantiate(Resources.Load("Free"));
        if (freeMessage)
        {
            // Turn the text on and puts it where your finger is
            Vector3 temporaryPosition = touchMarkerObjects[touchNumber].transform.position;
            temporaryPosition.z = defaultZ;
            temporaryPosition.y = temporaryPosition.y - 1f;
            freeMessage.transform.position = temporaryPosition;
            freeMessage.transform.parent = this.transform;
        }
	}
        
    // When someone taps the loot drop side we trigger this function
    public void LootDrop(Vector3 touchSpot)
    {
        if (lootPileBuffer > 0)
        {
            // Don't drop if buffer is > 0
        }
        else
        {
            lootPileBuffer = 0.5f; // Adds a 0.1s buffer so you can't double loot drop

            GameObject treasureObject = (GameObject)Instantiate(Resources.Load("TreasureObject"));
            if (treasureObject)
            {
                // Turn the treasure object on and put it where your finger is
                Vector3 temporaryPosition = touchSpot;
                temporaryPosition.z = defaultZ;
                treasureObject.transform.position = temporaryPosition;
                treasureObject.transform.parent = this.transform;
                treasureObject.transform.localScale = new Vector3(1, 1, 1);
                treasureObject.SetActive(true);
            }

            robberScore++;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.lootDropSFX); // Play gem loot drop sound
            }
    }

    public void LootPileGrow() // Increments the loot pile 
    {
        lootPileCount++;
        if (lootPileCount == Mathf.Floor(robberScoreWin * 0.2f)) // Every 20% of loot acquired the pile grows
        {
            LootPileAnimate();
            lootPileCount = 0;
        }
    }

    private void LootPileAnimate()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.treasureRumble);
        treasurePile.GetComponent<Animator>().SetTrigger("Next");
    }

    public void JailBreak() // When a robber lets go early
    {
        copScore += 1;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.jailBreakSFX);
        jailbreakEffect.SetActive(true);
    }

    // This is called when the game ends and you pass who the winner is (cops/robbers)
    void gameOver(string winner)
    {
        victor = winner;
        mainGameAnimator.SetTrigger("Next");
    }
        
    void ResetPivotRigidbodies(int touchNumber) 
    {
        var c = chainLinks[touchNumber];
            c.pivot.GetComponent<Rigidbody2D>().isKinematic = true;
            foreach(Rigidbody2D chainr in c.chainBodies)
            {
                chainr.GetComponent<HingeJoint2D>().enabled = true;
            }
        Vector3 TemporaryPosition = originalPivotPosition;
        TemporaryPosition.z = 10;
        c.pivot.transform.position = TemporaryPosition;

    }

}
