﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomPrizes : MonoBehaviour {

    public Text[] textOptions;
    public Image[] lights;
    public Text visibleText;

    public float lightSpeed = 0.05f;
    public float lightSpeedMax;
    public int lightCount = 0;

    void OnEnable()
    {
        foreach (Image i in lights)
        {
            i.enabled = false;
        }
        lightSpeedMax = lightSpeed;

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
