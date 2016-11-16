using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using BibaFramework.BibaGame;
using System.Collections.Generic;

public class CustomPrizes : MonoBehaviour {

    public List<string> textOptions = new List<string>();
    public Image[] lights;
    public Text visibleText;

    public float lightSpeed = 0.05f;
    public float lightSpeedMax;
    public int lightCount = 0;

    public GameController gameController;

    void OnEnable()
    {
        foreach (Image i in lights)
        {
            i.enabled = false;
        }
        lightSpeedMax = lightSpeed;

        // For each custom prize we add them to the list
        if (gameController.BibaDeviceSession.prize1 != "")
            textOptions.Add (gameController.BibaDeviceSession.prize1);
        if (gameController.BibaDeviceSession.prize2 != "")
            textOptions.Add(gameController.BibaDeviceSession.prize2);
        if (gameController.BibaDeviceSession.prize3 != "")
            textOptions.Add(gameController.BibaDeviceSession.prize3);
        if (gameController.BibaDeviceSession.prize4 != "")
            textOptions.Add(gameController.BibaDeviceSession.prize4);

        // Set the prize data to a random one of the prize options
        visibleText.text = textOptions[Random.Range(0, textOptions.Count-1)];
    }
    void OnDisable()
    {

    }
        
    void Update () {
        //visibleText = CustomPrizes.whateverIsChosen;
        if (lightSpeed > 0)
        {
            lightSpeed -= Time.deltaTime;
        }

        if (lightSpeed <= 0)
        {
            if (lightCount >= lights.Length)
                lightCount = 0;
            else
            {
                if (lightCount > 0) // As long as we're above zero turn the last one off
                    lights[lightCount - 1].GetComponent<Image>().enabled = false;
                else // If we're at zero make sure the highest # is off
                    lights[lights.Length-1].GetComponent<Image>().enabled = false;

                // Then we turn the light on
                lights[lightCount].GetComponent<Image>().enabled = true;
                lightCount++;
            }
            lightSpeed = lightSpeedMax;
        }
    }
}
