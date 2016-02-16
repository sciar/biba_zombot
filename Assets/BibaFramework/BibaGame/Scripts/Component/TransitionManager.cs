using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TransitionManager : Singleton<TransitionManager> {

	public void FadeTo(CanvasGroup canvasGroup, float targetAlpha, float duration, float delay, Action callback) {
		StartCoroutine(C_FadeTo(canvasGroup, targetAlpha, duration, delay, callback));
	}

	IEnumerator C_FadeTo(CanvasGroup canvasGroup, float targetAlpha, float duration, float delay, Action callback) {
		if (delay > 0f) {
			yield return new WaitForSeconds(delay);
		}
		float currentAlpha = canvasGroup.alpha;
		for (float f = 0f; f <= 1f; f += (Time.deltaTime/duration)) {
			canvasGroup.alpha = (Mathf.Lerp(currentAlpha, targetAlpha, f));
			yield return null;
		}
		canvasGroup.alpha = targetAlpha;
		if (callback != null) {
			callback ();
		}
	}

}
