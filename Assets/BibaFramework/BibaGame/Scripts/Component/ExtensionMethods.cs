using System;
using System.Collections;
using BibaFramework.Utility;
using UnityEngine;
using BibaFramework.BibaGame;

public static class ExtensionMethods 
{
	public static void FadeAlphaTo(this CanvasGroup canvasGroup, float targetAlpha, float duration = 1f, float delay = 0f, Action callback = null) 
	{
		if (canvasGroup != null) 
		{
			var fadeToBehaviour = canvasGroup.gameObject.AddComponent<FadeBehaviour> ();
			fadeToBehaviour.FadeTo (canvasGroup, targetAlpha, duration, delay, callback);
		}
	}

	public static void EnableInteraction(this CanvasGroup canvasGroup, bool value) 
	{
		if (canvasGroup != null) 
		{
			canvasGroup.blocksRaycasts = value;
			canvasGroup.interactable = value;
		}	
	}

	public static void EnableEmission(this ParticleSystem particleSystem, bool enabled)
	{
		var emission = particleSystem.emission;
		emission.enabled = enabled;
	}

	public static float GetEmissionRate(this ParticleSystem particleSystem)
	{
		return particleSystem.emission.rate.constantMax;
	}

	public static void SetEmissionRate(this ParticleSystem particleSystem, float emissionRate)
	{
		var emission = particleSystem.emission;
		var rate = emission.rate;
		rate.constantMax = emissionRate;
		emission.rate = rate;
	}
}
