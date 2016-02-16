using UnityEngine;
using System.Collections;
using BibaFramework.BibaGame;

public class PlayBGMOnBibaGameStateEnter : MonoBehaviour {

	public BibaCanvasGroup group;
	public GameView view;
	public string bgmString;

	void Awake() {
		group.onCanvasGroupEnterSuscribers += PlayBGM;
	}

	void PlayBGM() {
		view.AudioServices.PlayBGM (bgmString);
	}
}
