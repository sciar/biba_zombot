using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class TakeScreenshot : MonoBehaviour {
    
    public GameObject photoTakenText;
    public void screenShotTake(){
        // Turns on the text that pops up when you take a picture
        if (photoTakenText.activeSelf == false)
            photoTakenText.SetActive(true);

        // Using the ScreenshotManager plugin we name and take a screenshot (It auto appends date/time at the end)
        var fileName = "cnrScreenshot";
        ScreenshotManager.SaveScreenshot(fileName,"CNR");

        // Old version may come in handy
        //var saveFilePath = Path.Combine(Application.persistentDataPath, DateTime.UtcNow.ToLongTimeString());
        //if (File.Exists(saveFilePath))
        //{s
            // Do nothing
        //}
        //else // Take a screenshot
        //{
            //if (Application.platform == RuntimePlatform.IPhonePlayer)
            //Application.CaptureScreenshot(saveFilePath);

                //Application.CaptureScreenshot(fileName);
            //else
                //Application.CaptureScreenshot(saveFilePath);
        //}
    }
}
