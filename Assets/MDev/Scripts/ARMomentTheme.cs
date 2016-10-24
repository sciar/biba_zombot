using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ARMomentTheme : MonoBehaviour {

    public Sprite[] bgOptionsRobbers;
    public Sprite[] bgOptionsCops;

    public Text themeText; // Text at the top 
    public GameObject themeBackground; // BG color only (hole for face)
    public GameObject mainBG; // BG from the game we turn off
    public GameObject ARCamera; // Camera
    public Transform bibletDisable; // The biblet thing is still on from the AR screen so we turn it off

    public BibaCanvasGroup bibaCanvasGroup;

    void OnEnable()
    {
        bibaCanvasGroup.onCanvasGroupEnterSuscribers += reset;
    }

    void OnDisable()
    {
        bibaCanvasGroup.onCanvasGroupEnterSuscribers -= reset;
        mainBG.SetActive(true);
        themeBackground.SetActive(false);
        ARCamera.SetActive(false);
    }

    public void reset()
    {
        DisableChildren(); // Disables biblet scan thing
        themeBackground.SetActive(true);
        if (GameManager.Instance.victor == "cops")
        {
            themeBackground.GetComponent<Image>().sprite = bgOptionsCops[Random.Range(0, bgOptionsCops.Length)];
        }
        else
        {
            themeBackground.GetComponent<Image>().sprite = bgOptionsRobbers[Random.Range(0, bgOptionsRobbers.Length)];
        }
        //themeBackground.GetComponent<Image>.image = - Setup a random image if we have a bunch
    }

    // Update is called once per frame
    void Update () {
        mainBG.SetActive(false);

        ARCamera.SetActive(true);
      
	}

    public void DisableChildren()  
    {    
        foreach (Transform child in bibletDisable)     
        {  
            child.gameObject.SetActiveRecursively(false);   
        }   
    }
}
