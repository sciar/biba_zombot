using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OrangeRobotRequest : MonoBehaviour {

	public Text requestText;
    private float disableTimer;
    private float disableTimerMax = 5f;
    private int randomizeText;
    public string[] textOptions;
    public Sprite[] imageOptions;
    public bool currentlyVisible; // Variable to check if the bot is currently visible on screen

    public Animator anim;

    public AudioClip cbVoice1;
    public AudioClip cbVoice2;
    public AudioClip cbVoice3;
    private AudioClip cbVoiceChoice;

    private bool audioPlayed; // Simple way to track playing the audio clip each consecutive appearance

	// Use this for initialization
	void OnEnable () {
        anim = this.transform.GetComponent<Animator>();
        disableTimer = disableTimerMax;

        randomizeText = Random.Range(0, textOptions.Length);
        audioPlayed = false; // Making sure the first appearance will play audio
	}
	
    void OnDisable(){

    }

    public void SetEquipmentText(string eText) // Set the text (MissionDeployment.cs sends which piece of equipment has been randomized)
    {
        GetComponent<Image>().sprite = imageOptions[Random.Range(0,imageOptions.Length)];
        requestText.text = textOptions[randomizeText] + " " + eText.ToUpper() +"!";
    }

    void PlayCBAudio(int i)
    {
        if (i == 1)
            cbVoiceChoice = cbVoice1;
        else if (i == 2)
            cbVoiceChoice = cbVoice2;
        else
            cbVoiceChoice = cbVoice3;
        AudioManager.Instance.PlaySingle(cbVoiceChoice);
    }

    void Update()
    {
        if (currentlyVisible)
        {
            if (audioPlayed == false && disableTimer < disableTimerMax * 0.8f) // Makes sure we give him a second to appear
            {
                PlayCBAudio(Random.Range(1,3));
                audioPlayed = true;
            }
            disableTimer -= Time.deltaTime;
        }
        if (disableTimer <= 0)
        {
            //run the animator to turn it off
            anim.SetTrigger("Next");
            currentlyVisible = false;
            audioPlayed = false;
            disableTimer = disableTimerMax;
        }   
    }
}
