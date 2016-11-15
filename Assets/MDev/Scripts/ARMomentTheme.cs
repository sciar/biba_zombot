using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ARMomentTheme : MonoBehaviour {

    public Sprite[] bgOptionsZombies;
    public Sprite[] bgOptionsSurvivors;

    public Text themeText; // Text at the top 
    public GameObject themeBackground; // BG color only (hole for face)
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
        themeBackground.SetActive(false);
        ARCamera.SetActive(false);
    }

    public void reset()
    {
        DisableChildren(); // Disables biblet scan thing
        themeBackground.SetActive(true);
        if (GameManager.Instance.victor == "Zombies")
        {
            themeBackground.GetComponent<Image>().sprite = bgOptionsZombies[Random.Range(0, bgOptionsZombies.Length)];
        }
        else
        {
            themeBackground.GetComponent<Image>().sprite = bgOptionsSurvivors[Random.Range(0, bgOptionsSurvivors.Length)];
        }
        //themeBackground.GetComponent<Image>.image = - Setup a random image if we have a bunch
    }

    // Update is called once per frame
    void Update () {

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
