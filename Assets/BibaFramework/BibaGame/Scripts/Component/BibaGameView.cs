using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BibaFramework.BibaMenu;
using BibaFramework.BibaGame;

public class BibaGameView : GameView {
	
	public Dictionary<BibaGameStatesEnum, BibaCanvasGroup> statesToBibaCanvasGroups;
	public BibaGameStatesEnum currentGameState;
	public Canvas canvas;
	public Animator animator;
	public GameController controller;

	public delegate void AnimationEventCollection(string eventType);
	public AnimationEventCollection AnimationEventSuscribers;

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();
		statesToBibaCanvasGroups = new Dictionary<BibaGameStatesEnum, BibaCanvasGroup> ();
		foreach (BibaCanvasGroup relayCanvasGroup in canvas.GetComponentsInChildren<BibaCanvasGroup>()) {
			if (!statesToBibaCanvasGroups.ContainsKey(relayCanvasGroup.currentState)) {
				relayCanvasGroup.bibaGameStateAnimator = GetComponent<Animator>();
				relayCanvasGroup.gameObject.SetActive(false);
				relayCanvasGroup.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
				statesToBibaCanvasGroups.Add(relayCanvasGroup.currentState, relayCanvasGroup);
			} else {
				Debug.LogWarning("Duplicate Canvas Group State found. Skipped");
			}
		}
	}

	public void sendAnimationEventCommand(string eventType) {
		if (AnimationEventSuscribers != null) {
			AnimationEventSuscribers (eventType);
		}
	}
}
