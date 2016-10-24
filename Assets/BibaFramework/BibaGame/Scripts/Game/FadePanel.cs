using UnityEngine;
using System.Collections;

public class FadePanel : MonoBehaviour {

	public CanvasGroup canvasGroup;

	public void FadeTo(float target) {
		canvasGroup.FadeAlphaTo (target, 0.5f, 0f);
	}

	public void SetAnimator(bool value) {
		GetComponent<Animator> ().SetBool ("Enabled", value);
	}
}
