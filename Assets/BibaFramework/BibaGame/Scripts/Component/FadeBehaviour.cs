using System;
using UnityEngine;
using System.Collections;

namespace BibaFramework.BibaGame
{
	public class FadeBehaviour : MonoBehaviour
	{
		public void FadeTo(CanvasGroup canvasGroup, float targetAlpha, float duration, float delay, Action callback)
		{
			StartCoroutine(C_FadeTo(canvasGroup,targetAlpha,duration,delay,callback));
		}

		IEnumerator C_FadeTo(CanvasGroup canvasGroup, float targetAlpha, float duration, float delay, Action callback) 
		{
			float currentAlpha = canvasGroup.alpha;

			if (delay > 0f) 
			{
				yield return new WaitForSeconds(delay);
			}

			for (float f = 0f; f <= 1f && canvasGroup != null; f += (Time.deltaTime/duration)) 
			{
				canvasGroup.alpha = (Mathf.Lerp(currentAlpha, targetAlpha, f));
				yield return null;
			}

			canvasGroup.alpha = targetAlpha;

			if (callback != null) 
			{
				callback ();
			}

			Destroy (this);
		}

		public void FadeTo(SpriteRenderer spriteRenderer, float targetAlpha, float duration, float delay, Action callback)
		{
			StartCoroutine(C_FadeTo(spriteRenderer,targetAlpha,duration,delay,callback));
		}

		IEnumerator C_FadeTo(SpriteRenderer spriteRenderer, float targetAlpha, float duration, float delay, Action callback) 
		{
			float currentAlpha = spriteRenderer.color.a;

			if (delay > 0f) 
			{
				yield return new WaitForSeconds(delay);
			}

			for (float f = 0f; f <= 1f && spriteRenderer != null; f += (Time.deltaTime/duration)) 
			{
				spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,(Mathf.Lerp(currentAlpha, targetAlpha, f)));
				yield return null;
			}

			spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetAlpha);

			if (callback != null) 
			{
				callback ();
			}

			Destroy (this);
		}
	}
}