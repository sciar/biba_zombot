using UnityEngine;
using System.Collections;
using System;

public static class ExtensionMethods {
	public static void FadeAlphaTo(this CanvasGroup canvasGroup, float targetAlpha, float duration = 1f, float delay = 0f, Action callback = null) {
		TransitionManager.Instance.FadeTo (canvasGroup, targetAlpha, duration, delay, callback);
	}
	public static void EnableInteraction(this CanvasGroup canvasGroup, bool value) {
		canvasGroup.blocksRaycasts = value;
		canvasGroup.interactable = value;
	}
}
