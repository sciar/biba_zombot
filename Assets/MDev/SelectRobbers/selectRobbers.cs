using UnityEngine;
using System.Collections;

public class selectRobbers : MonoBehaviour {

    public GameObject[] copObjects;
    public GameObject[] robberObjects;

    public int playerCount;

    public UnityEngine.UI.Button nextButton;

    // Math for how much 
    private float copPercentage = 0.3f;

	// Use this for initialization
	void Start () {

        // Safety check to make sure we don't have anything selected for our first time on the screen
	    foreach (GameObject cops in copObjects)
        {
            cops.SetActive(false);
        }

        foreach(GameObject robbers in robberObjects)
        {
            robbers.SetActive(false);
        }
	}
	
    void Update()
    {
        if (playerCount == 0) { nextButton.interactable = false; }
        else { nextButton.interactable = true; }
    }

    // Lets our buttons change how many players there are
    public void setPlayerCount(int pCount)
    {
        playerCount = pCount;
        updateUI(); // Updates the visuals

        
    }

    public void updateUI()
    {
        // Turn off the cop and robber objects
        foreach (GameObject cops in copObjects)
        {
            cops.SetActive(false);
        }

        foreach (GameObject robbers in robberObjects)
        {
            robbers.SetActive(false);
        }

        // We record how many cops we activated
        var copCount = Mathf.Clamp(Mathf.Ceil(playerCount * 0.3f), 0, 3); // Cops make up 30% and up to 3
        if (playerCount == 7) { copCount = 2; }; // Hard coding to set cops to 2 on 7 since those teams would be imbalanced

        // Now we turn them on 
        for (int i = 0; i < copCount; i++)
        {
            copObjects[i].SetActive(true);
        }

        for (int i = 0; i < playerCount - copCount; i++) // Then we activate robbers for the remaining value of players
        {
            robberObjects[i].SetActive(true);
        }
    }

}
