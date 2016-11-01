using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OrangeRobotRequest : MonoBehaviour {

	public Text requestText;
    private float disableTimer;
    private int randomizeText;
    public string[] textOptions;
    public bool currentlyVisible; // Variable to check if the bot is currently visible on screen

    public Animator anim;

	// Use this for initialization
	void OnEnable () {
        anim = this.transform.GetComponent<Animator>();
        disableTimer = 5f;

        randomizeText = Random.Range(0, textOptions.Length);
	}
	
    void OnDisable(){

    }

    public void SetEquipmentText(string eText) // Set the text (MissionDeployment.cs sends which piece of equipment has been randomized)
    {
        
        requestText.text = textOptions[randomizeText] + " " + eText.ToUpper() +"!";
    }
    void Update()
    {
        if (currentlyVisible)
            disableTimer -= Time.deltaTime;
        if (disableTimer <= 0)
        {
            //run the animator to turn it off
            anim.SetTrigger("Next");
            currentlyVisible = false;
            disableTimer = 5f;
        }   
    }
}
