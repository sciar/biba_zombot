using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using BibaFramework.BibaGame;

public class MissionDeployment : MonoBehaviour {

    private float growthRate = 0.1f;
    private bool grow = true;

	public string missionText;

    public GameController gameController;

    void OnEnable()
    {
        gameController = GameObject.Find("Game").GetComponent<GameController>();
        var selectedEquipments = gameController.BibaDeviceSession.SelectedEquipments;
        missionText = gameController.LocalizationService.GetText("equipment_"+selectedEquipments[Random.Range(0, selectedEquipments.Count)].EquipmentType.ToString());
        transform.localScale = new Vector3(0, 0, 0);

        // For some reason it seems to happily comply as long as we force the text in Update and not start
        this.gameObject.GetComponent<Text>().text = missionText;
        GameManager.Instance.missionText = missionText; // Sets it up so the GameManager can communicate with the orange robot request
    }

    void OnDisable()
    { // Destroys itself when the player lets go
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        // Code to make the text grow into place
        if (transform.localScale.x >= 1)
            grow = false;
        
        if (grow)
            transform.localScale += Vector3.one * growthRate;

    }
}
