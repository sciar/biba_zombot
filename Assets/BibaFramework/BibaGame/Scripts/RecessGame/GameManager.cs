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

	public Camera mainCam;
	public ParticleSystem explosionEmmiter;

    // New Version
    public UnityEngine.AudioClip gameMusic;
    private UnityEngine.AudioClip originalBGMusic;
    public bool win; // Check to make sure you can only win once
    public float gameTime; // How long we've been on this screen this round

    // Z Axis Fix on Screen Touch Updates
    private float defaultZ;

    public string victor; // Called by other scripts to check who won (Probably only the the win screen)
    public BibaCanvasGroup bibaCanvasGroup;

    public Animator bibaRootAnimator; // Used to check for the variables that are active

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
        defaultZ = touchMarkerObjects[0].transform.position.z; // Sets up the default Z position for the touch marker objects

        foreach (GameObject g in touchMarkerObjects)
        {
            g.SetActive(false);
        }
            
        textGroup.FadeAlphaTo (1f, 0.25f);

        bibaRootAnimator = GameObject.Find("MenuStateMachine").GetComponent<Animator>();

        if (bibaRootAnimator.GetBool("ShowTagScan") == true) // If they've selected yes go to the AR moments
            mainGameAnimator.SetBool("BibaPlayground", true);
    }

	void Update() 
    {
        
        gameTime += Time.deltaTime; // Increment Gametime

        // Arrest Touches
        int touchCount = Input.touchCount;

        if (touchCount > 0)
        {
           for (int i = 0; i < touchCount; i++)
           {     
                Touch touch = Input.GetTouch(i);
                TouchPhase phase = touch.phase;

                switch (phase)
                {
                    case TouchPhase.Began: // This triggers when a player first touches the screen
                        //Debug.LogError("New touch detected " + touch.fingerId);
                        StartTouch(touch.fingerId);
                        break;
                    case TouchPhase.Ended: // This triggers when a player lets go of their touch
                        //Debug.LogError("Touch " + touch.fingerId + " Has Ended");
                        StopTouch(touch.fingerId);
                        break;
                    case TouchPhase.Canceled: // This case should only trigger if someone tries to palm the full screen or too many touches are detected (aka 9+)
                        Debug.LogError("Touch index " + touch.fingerId + " cancelled");
                        break;
                }
                Vector3 temporaryPosition = mainCam.ScreenToWorldPoint(Input.GetTouch(i).position);
                temporaryPosition.z = defaultZ;
                touchMarkerObjects[touch.fingerId].transform.position = temporaryPosition;
            }
        }
        else
        {
            textGroup.FadeAlphaTo (1f, 0.25f); // Fade the text back in since we have zero touches
        }

        //if (roundTimer == 0) { gameOver("cops"); win = true; }; // Cop victory condition
	}

	void StartTouch(int fingerTracker)
	{ // On touching the screen we spawn a circle
        textGroup.FadeAlphaTo (0f, 0.25f); // This is the canvas with the text "Jail" in it and we are fading it out
        touchMarkerObjects[fingerTracker].SetActive(true);
        //touchMarkerObjects[fingerTracker].GetComponent<JailTimer>().touchNumber = fingerTracker; // Sends the finger track # to the script
        AudioManager.Instance.PlaySFX(AudioManager.Instance.chainSFX); // Play chain sound when you get an arrest
	}
    void StopTouch(int fingerTracker)
    {
        touchMarkerObjects[fingerTracker].SetActive(false);

        explosionEmmiter.Emit(40);
        // Tells the player they're free with some text at their finger spot
        GameObject freeMessage = (GameObject)Instantiate(Resources.Load("Free"));
        if (freeMessage)
        {
            // Turn the text on and puts it where your finger is
            Vector3 temporaryPosition = touchMarkerObjects[fingerTracker].transform.position;
            temporaryPosition.z = defaultZ;
            temporaryPosition.y = temporaryPosition.y - 1f;
            freeMessage.transform.position = temporaryPosition;
            freeMessage.transform.parent = this.transform;
        }
    }
        
    // This is called when the game ends and you pass who the winner is (cops/robbers)
    void gameOver(string winner)
    {
        victor = winner;
        mainGameAnimator.SetTrigger("Next");
    }

}