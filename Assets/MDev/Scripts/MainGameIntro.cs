using UnityEngine;
using System.Collections;

public class MainGameIntro : MonoBehaviour {

    public AudioClip doorCreaking;

	// Use this for initialization
	void OnEnable () {
        AudioManager.Instance.PlaySFX(doorCreaking);
	}
	
}
