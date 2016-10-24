using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JailTimer : MonoBehaviour {

    private float timer = 10f;
    private float explodeTimer;
    private float explodeTimerMax = 0.1f;
    private bool explodeActive = false;
    private bool explodeOnce = false;
    private bool active = true;
    //public Text text; - Used if we want to have a text countdown timer
    public Image radialCountdownImage;
    public Text countdownText;
    public int touchNumber; // Tracks which finger touch this timer is attached to

	// Use this for initialization
	void OnEnable () {
        timer = 10f;
        active = true;
        explodeActive = false;
        explodeOnce = false;
        explodeTimer = explodeTimerMax;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0 && active == true)
        {
            timer -= Time.deltaTime;
            radialCountdownImage.fillAmount = timer/10;
            if (countdownText) // Safety check in case we don't bind this
                countdownText.text = Mathf.Ceil(timer).ToString();
        }
        else if (active == true)
        {
            timer = 10;
            active = false;
            Disable();
        }

        if (timer <= explodeTimerMax && explodeActive == false) // For our final second explode the chains
        {
            explodeActive = true;
            explodeOnce = true;
        }

        if (explodeActive) // Once the explosion has happened we count down 1s
        {
            explodeTimer -= Time.deltaTime;
            if (explodeOnce)
            {
                GameManager.Instance.ExplodeTouch(touchNumber); // Tells the GM to explode this ones chains
                explodeOnce = false;
            }

        }
           
	}

    void Disable() // Disables the GameObject so the GameManager knows what to do
    {
        this.gameObject.SetActive(false);         
    }

}
