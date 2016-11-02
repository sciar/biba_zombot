using UnityEngine;
using System.Collections;

public class Prizes : MonoBehaviour {

    public GameObject prizeSelectionScreen;
    public GameObject prizeInstructionScreen;

    public void TransitionToPrizeScreen()
    {
        if (prizeSelectionScreen.activeSelf == false) // Turns on / off based on current visible screen
            prizeSelectionScreen.SetActive(true);
        else
            prizeSelectionScreen.SetActive(false);

        if (prizeInstructionScreen.activeSelf == true) // Turns on / off based on current visible screen
            prizeInstructionScreen.SetActive(false);
        else
            prizeInstructionScreen.SetActive(true);
    }

    public void SavePrizeData()
    {

    }
}
