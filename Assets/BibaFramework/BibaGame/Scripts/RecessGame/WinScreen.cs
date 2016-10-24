using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using BibaFramework.BibaGame;

public class WinScreen : MonoBehaviour {

    public Text victoryText;
    public Image image;
    public BibaDevice bibaDevice;
    public GameController controller; 
    	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.victor == "cops") { victoryText.text = "COPS WIN!"; image.color = Color.blue; } //LOCALIZATION controller.LocalizationService.GetText("copWin");
        else if (GameManager.Instance.victor == "robbers") { victoryText.text = "ROBBERS WIN!"; image.color = Color.red; } //controller.LocalizationService.GetText("robberWin");
	}
}