using UnityEngine;
using System.Collections;

public class MainGameIntro : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        AudioManager.Instance.PlaySingle(AudioManager.Instance.doorCreak);
	}
	
}
