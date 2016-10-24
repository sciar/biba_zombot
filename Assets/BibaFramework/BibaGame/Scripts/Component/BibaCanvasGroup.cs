using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CanvasGroup))]

public class BibaCanvasGroup : MonoBehaviour {
	[HideInInspector] public CanvasGroup canvasGroup
    {
        get {
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
                return _canvasGroup;
            }
            else { return _canvasGroup; }
        }
    }
	[HideInInspector] public Animator bibaGameStateAnimator;
	public BibaGameStatesEnum currentState;

	public float entryDelay = 0.5f;
	public float entryFadeDuration = 0.5f;
	public float exitDelay = 0f;
	public float exitFadeDuration = 0.5f;

	public delegate void OnCanvasGroupActivation();
	public OnCanvasGroupActivation onCanvasGroupEnterSuscribers;

    private CanvasGroup _canvasGroup;

	public void CanvasGroupIsActive() {
		if (onCanvasGroupEnterSuscribers != null) {
			onCanvasGroupEnterSuscribers ();
		}
	}


}
