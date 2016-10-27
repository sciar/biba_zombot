using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OrangeRobotRequest : MonoBehaviour {

    public Text requestText;
    private float disableTimer;

	// Use this for initialization
	void OnEnable () {
        disableTimer = 3f;
	}
	
    void OnDisable(){

    }

    public void SetEquipmentText(string eText) // Set the text (MissionDeployment.cs sends which piece of equipment has been randomized)
    {
        requestText.text = eText;
    }
    void Update()
    {
        disableTimer -= Time.deltaTime;
        if (disableTimer <= 0)
        {
            //run the animator to turn it off
        }   
    }
}
