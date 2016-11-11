using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BibaFramework.BibaGame;
using UnityEngine.UI;

[RequireComponent (typeof (BibaCanvasGroup))]

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    // Big stack of variables that are used for various things
    public AudioServices audioServices;
	public Animator mainGameAnimator;
	public CanvasGroup textGroup; // Text with Jail on it that fades when you have someone in jail
	public GameObject[] touchMarkerObjects;
	public Camera mainCam;
	public ParticleSystem explosionEmmiter;
    public UnityEngine.AudioClip gameMusic;
    private UnityEngine.AudioClip originalBGMusic;
    public bool win; // Check to make sure you can only win once
    public float gameTime; // How long we've been on this screen this round
    public GameObject preGameText;
    private float gameStartTimer; // Whether or not we've started
    public GameObject gameStartGO; // Object that holds the timer

    // Game Timer
    public GameObject STContainer;
    public GameObject survivorTimer;
    public int survivorTimerMax = 5; // Tracked in full minutes
    private float sTimerMinutes;
    private float sTimerSeconds;

    // No Survivors Left
    public GameObject noSurvivors;
    private float noSurvivorsTimer;
    private int noSurvivorsTimerMax = 60;

    // Active player tracking list
    public bool[] activeTouchList = new bool[8]; // Creates an array with 8 options

    // Reset Variables for particle system
    private Color originalParticleColor;
    private float originalParticleSize;

    // Tracking who currently has missions
    private bool[] missionActiveList = new bool[8];

    // Send Rate
    private float sendRate;

    // Helper Variables
    private int helperCounter;
    private int helperCounterTriggerValue;
	public string missionText;
    public GameObject helperRobot;

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

	public void SetSurvivorTimer(int totalTime) // Called in the time select screen to set overall game time
	{
        survivorTimerMax = totalTime;
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
        gameStartTimer = 5; // Countdown to let the game start
        noSurvivorsTimer = noSurvivorsTimerMax;
        gameStartGO.GetComponent<Text>().text = Mathf.CeilToInt(gameStartTimer).ToString(); // Set the game timer immediately upon loading in
        gameStartGO.SetActive(true);
        preGameText.SetActive(true);

        // Particle Reset Variables
        originalParticleColor = touchMarkerObjects[0].GetComponentInChildren<ParticleSystem>().startColor;
        originalParticleSize = touchMarkerObjects[0].GetComponentInChildren<ParticleSystem>().startSize;

        helperCounterTriggerValue = Random.Range(2,4); // Reset the random value so we don't know which rounds he'll come out on
        helperCounter = helperCounterTriggerValue; // Reset how many times we've called the helper

        noSurvivors.SetActive(false); // Make sure the no survivors warning is off

        // Setting the survivor timer
        sTimerMinutes = survivorTimerMax;
        sTimerSeconds = 0f;
        survivorTimer.GetComponent<Text>().text = "Survivor Timer " + Mathf.FloorToInt(sTimerMinutes).ToString("00") + ":" + Mathf.FloorToInt(sTimerSeconds % 60).ToString("00");
        survivorTimer.GetComponent<Text>().CrossFadeAlpha(0,0f,false);
        // MONDAY ---------------!!?!?!??!?!?????????- Resize the STContainer gameobject instead of this text object ---------------!!?!?!??!?!?????????
        Debug.LogError("MATT FIX HERE");

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

		if (bibaRootAnimator.GetBool ("CustomPrizes") == true)
			mainGameAnimator.SetBool ("CustomPrizes", true);
    }

	void Update() 
    {
        gameTime += Time.deltaTime; // Increment Gametime
        // Total Amount of Touches
        int touchCount = Input.touchCount;

        if (touchCount > 0)
        {
            // Sending players to equipment and the frequency at which it occurs = sendRate
            if (sendRate > 0 && gameStartTimer <= 0)
            {
                sendRate -= Time.deltaTime;
            }
            else if (gameStartTimer <= 0)
            {
                var playerToSend = Random.Range(0, activeTouchList.Length);
                bool touchExistSafety = false;

                // We run another safety check to make sure that the array has at least one true value
                for (int i = 0; i < activeTouchList.Length; i++) {
                    if (activeTouchList[i])
                        touchExistSafety = true;
                }

                // Then we reroll until we get one of those values
                while (!activeTouchList[playerToSend] && touchExistSafety) //&& !missionActiveList[playerToSend]
                {
                    playerToSend = Random.Range(0, activeTouchList.Length);
                }

                // Sends the data to turn on the mission stuff
                if (!missionActiveList[playerToSend])
                {
                    missionActiveList[playerToSend] = true; // Adds the current mission holder to a list so we wont get duplicates
                    touchMarkerObjects[playerToSend].GetComponentInChildren<ParticleSystem>().startColor = new Color(255,0,0,1); // Set the color to red
                    touchMarkerObjects[playerToSend].GetComponentInChildren<ParticleSystem>().startSize = 1.6f;
                    touchMarkerObjects[playerToSend].GetComponent<TouchObjects>().turnOnShadingBox(); // Turns on the box and mission text
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.missionPopup);// Adds audio to the mission popup
                }
                    
                // After we've made sure everything checks out we send a kid to a piece of equipment
                /*GameObject freeMessage = (GameObject)Instantiate(Resources.Load("Free"));
                if (freeMessage && !missionActiveList[playerToSend]) // Wont spawn a mission on a player with a current mission
                {
                    // Turn the text on and puts it where your finger is
                    Vector3 temporaryPosition = touchMarkerObjects[playerToSend].transform.position;
                    temporaryPosition.z = defaultZ;
                    temporaryPosition.y = temporaryPosition.y - 1.5f;
                    freeMessage.transform.position = temporaryPosition;
                    freeMessage.transform.parent = touchMarkerObjects[playerToSend].transform;
                    missionActiveList[playerToSend] = true; // Adds the current mission holder to a list so we wont get duplicates
                    touchMarkerObjects[playerToSend].GetComponentInChildren<ParticleSystem>().startColor = new Color(255,0,0,1); // Set the color to red
                    touchMarkerObjects[playerToSend].GetComponentInChildren<ParticleSystem>().startSize = 1.6f;
                    touchMarkerObjects[playerToSend].GetComponent<TouchObjects>().turnOnShadingBox(); // Turns on the box to provide a bounding box behind the text
                }*/

                // Then we turn on the helper guy
                helperCounter++;
                if (helperCounter >= helperCounterTriggerValue)
                {
                    if (missionText != null) // MissionDeployment.cs sends the mission text over that it randomizes
                        helperRobot.GetComponent<OrangeRobotRequest>().SetEquipmentText(missionText.ToString()); // We send this to the robot so he knows the latest mission text

                    // Turn on the Robot (Or make sure he's on)
                    helperRobot.SetActive(true);
                    // Play the Radio SFX when orange robot comes in
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.radioCall);
                    // Trigger him to cmon in
                    helperRobot.GetComponent<Animator>().SetTrigger("Next");
                    helperRobot.GetComponent<OrangeRobotRequest>().currentlyVisible = true;
                    helperCounter = 0; // Reset the counter so we now have to build up to the trigger value again
                    helperCounterTriggerValue = Random.Range(3,6); // Reset the random value so we don't know which rounds he'll come out on
                }
                   
                sendRate = Random.Range(3.0f, 8.0f);
            }


            noSurvivorsTimer = noSurvivorsTimerMax; // Reset the no survivors timer since we are now tracking a survivor
            noSurvivors.SetActive(false);

            // Check if the survivor timer is on if not turn it on as long as the game has started
            if (gameStartTimer <= 0)
                survivorTimer.GetComponent<Text>().CrossFadeAlpha(1,0.5f,false);

            if (gameStartTimer > 0) // This updates the countdown timer as long as someone is pressing on the screen we begin the game
            {
                gameStartTimer -= Time.deltaTime;
                gameStartGO.GetComponent<Text>().text = Mathf.CeilToInt(gameStartTimer).ToString();
            }
            else
                StartGame();

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
        else // If no screen touches
        {
            if (gameStartTimer <= 0 && noSurvivorsTimer > 0) // make sure the game has started
            {// Display the warning that there are no survivors left and they must return within X time
                noSurvivors.SetActive(true);
                noSurvivorsTimer -= Time.deltaTime;
                noSurvivors.GetComponentInChildren<Text>().text = "Return Timer: " +Mathf.RoundToInt(noSurvivorsTimer).ToString();
            }
            if (sendRate < 5) // Just a check if the send rate is under five and nobody is back we give them a small time buffer
            {
                sendRate = 6;
            }

            survivorTimer.GetComponent<Text>().CrossFadeAlpha(0,0.5f,false); // Fade the timer out so we can display the return timer
        }

        // IF GAME HAS STARTED WE ARE CONTROLLING THE ROUND TIMER
        if (gameStartTimer <= 0)
        {
            if (sTimerMinutes > 0) // Stopping the countdown in case we have no survivors at the safehouse and the timer hits 0
                sTimerSeconds -= Time.deltaTime;
            if (sTimerSeconds <= 0)
            {
                if (sTimerMinutes == 0 && touchCount > 0) // If the survivors last the full time
                    gameOver("Survivors");
                
                sTimerSeconds = 60; // Set the seconds to 60 once we roll over a minute
                sTimerMinutes--;
            }
            survivorTimer.GetComponent<Text>().text = "Survivor Timer " + Mathf.FloorToInt(sTimerMinutes).ToString("00") + ":" + Mathf.FloorToInt(sTimerSeconds % 60).ToString("00");
        }
            
        // NO SURVIVORS LEFT FOR X TIME
        if (noSurvivorsTimer <= 0) // If you ever run out of time on the Survivor Timer the Zombies win
        {
            gameOver("Zombies");
            win = true;
        }
	}

	void StartTouch(int fingerTracker)
	{ // On touching the screen we spawn a circle
        textGroup.FadeAlphaTo (0f, 0.25f); // This is the canvas with the text "Jail" in it and we are fading it out
        touchMarkerObjects[fingerTracker].SetActive(true);
        //touchMarkerObjects[fingerTracker].GetComponent<JailTimer>().touchNumber = fingerTracker; // Sends the finger track # to the script
        activeTouchList[fingerTracker] = true; // Updates our list of active touches
	}

    void StopTouch(int fingerTracker)
    {
        touchMarkerObjects[fingerTracker].SetActive(false); // Turns off the particles under your finger

        AudioManager.Instance.PlaySFX(AudioManager.Instance.poofSFX); // Play poof noise when somebody lets go

        explosionEmmiter.Emit(40);
        Vector3 temporaryPosition = touchMarkerObjects[fingerTracker].transform.position;
        temporaryPosition.z = defaultZ;
        temporaryPosition.y = temporaryPosition.y;
        explosionEmmiter.transform.position = temporaryPosition;

        activeTouchList[fingerTracker] = false; // Updates our list of active touches
        missionActiveList[fingerTracker] = false; // Tells us this touch # doesn't currently have a mission

        touchMarkerObjects[fingerTracker].GetComponentInChildren<ParticleSystem>().startColor = originalParticleColor; // Reset to default
        touchMarkerObjects[fingerTracker].GetComponentInChildren<ParticleSystem>().startSize = originalParticleSize;
        touchMarkerObjects[fingerTracker].GetComponent<TouchObjects>().turnOffShadingBox(); // Turn off the shading box once you've removed your finger
    }

    public void StartGame()
    {
        // Once you hit start begin the roulette
        preGameText.SetActive(false);
        gameStartGO.SetActive(false); // Let the GameManager know the games started

    }
        
    // This is called when the game ends and you pass who the winner is (cops/robbers)
    void gameOver(string winner)
    {
        victor = winner;
        mainGameAnimator.SetTrigger("Next");
    }

}