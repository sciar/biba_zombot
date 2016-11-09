using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonClickCheck : MonoBehaviour {

    public Button nextButton;

	// Use this for initialization
	void OnEnable () {
        nextButton.interactable = false;
	}

    public void ButtonOn()
    {
        nextButton.interactable = true;
    }
}
