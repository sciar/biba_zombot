using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Jailbreak : MonoBehaviour {

    private float timer;
    public GameObject rightTriangle;
    public GameObject leftTriangle;
    private int framesBetweenColorSwap;
    private Color cRed = new Color(255, 0, 0, 180);
    private Color cBlue = new Color(0,0,255,180);

    private float rotationSpeed = 300f;

    void OnEnable(){
        //Reset the timer
        timer = 1.8f;
        rightTriangle.GetComponent<Image>().color = cBlue; // blue
        leftTriangle.GetComponent<Image>().color = cRed; // red
    }
       	
	// Update is called once per frame
	void Update () {
        rightTriangle.transform.Rotate(new Vector3(0,0,-1) * rotationSpeed * Time.deltaTime);
        leftTriangle.transform.Rotate(new Vector3(0,0,-1) * rotationSpeed * Time.deltaTime);

        framesBetweenColorSwap++;

        if (framesBetweenColorSwap > 10) // Tracks each frame and does a color swap if above the number
        {
            framesBetweenColorSwap = 0;
            if (rightTriangle.GetComponent<Image>().color == cBlue)
                rightTriangle.GetComponent<Image>().color = cRed;
            else
                rightTriangle.GetComponent<Image>().color = cBlue;

            if (leftTriangle.GetComponent<Image>().color == cBlue)
                leftTriangle.GetComponent<Image>().color = cRed;
            else
                leftTriangle.GetComponent<Image>().color = cBlue;

        }

	    // This is where we spin the effect and then we'll run a timer and turn it off
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false); // Turn this object off if it's been 3s
        }
    }
}
