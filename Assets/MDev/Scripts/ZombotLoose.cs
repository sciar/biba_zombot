using UnityEngine;
using System.Collections;

public class ZombotLoose : MonoBehaviour {

    public BibaCanvasGroup bibaCanvasGroup;

	// Use this for initialization
	void OnEnable () {
        bibaCanvasGroup.onCanvasGroupEnterSuscribers += playMoan;
	}
    void OnDisable()
    {
        bibaCanvasGroup.onCanvasGroupEnterSuscribers -= playMoan;
    }

    void playMoan(){
        //AudioManager.Instance.PlaySFX(AudioManager.Instance.zombieMoan);
    }
	
}
