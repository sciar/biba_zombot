using UnityEngine;
using System.Collections;
using BibaFramework.BibaGame;

public class PlaySFXOnStateEnter : MonoBehaviour {

	public string sfxName;
	private AudioServices audioServices;

	void Awake() {
		audioServices = GameObject.FindObjectOfType<AudioServices> ();
		GetComponent<BibaCanvasGroup> ().onCanvasGroupEnterSuscribers += OnStateEnter;
	}

	void OnStateEnter() {
		audioServices.PlaySFXWhileFadingActiveBGM (sfxName);
	}
}
