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
        if (GameManager.Instance.victor == "Survivors") { victoryText.text = "SURVIVORS WIN!"; image.color = Color.blue; } //LOCALIZATION controller.LocalizationService.GetText("copWin");
        else if (GameManager.Instance.victor == "Zombies") { victoryText.text = "ZOMBIES WIN!"; image.color = Color.red; } //controller.LocalizationService.GetText("robberWin");
	}
}