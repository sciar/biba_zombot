using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using BibaFramework.BibaMenu;

[RequireComponent (typeof(Button))]

public class PlaySFXOnClick : MonoBehaviour {

	public SceneMenuStateView view;
    public UnityEngine.AudioClip sfxString;
	
	void Awake() {
		GetComponent<Button> ().onClick.AddListener (Clicked);
	}

	void Clicked() {
		view.AudioServices.PlaySFX (sfxString);
	}
}
