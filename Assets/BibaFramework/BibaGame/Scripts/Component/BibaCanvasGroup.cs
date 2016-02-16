using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CanvasGroup))]

public class BibaCanvasGroup : MonoBehaviour {
	[HideInInspector] public CanvasGroup canvasGroup;
	[HideInInspector] public Animator bibaGameStateAnimator;
	public BibaGameStatesEnum currentState;

	public float entryDelay = 0.5f;
	public float entryFadeDuration = 0.5f;
	public float exitDelay = 0f;
	public float exitFadeDuration = 0.5f;

	public delegate void OnCanvasGroupActivation();
	public OnCanvasGroupActivation onCanvasGroupEnterSuscribers;

	void Awake() {
		canvasGroup = GetComponent<CanvasGroup> ();
	}

	public void CanvasGroupIsActive() {
		if (onCanvasGroupEnterSuscribers != null) {
			onCanvasGroupEnterSuscribers ();
		}
	}


}
