using System;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public abstract class BaseObjectMenuStateView : View 
	{
		public CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }

		public Animator Animator { get { return GetComponent<Animator> (); } }

		public virtual void AnimateEntry()
		{
			gameObject.SetActive(true);
			if (Animator != null && Animator.runtimeAnimatorController != null) 
			{
				StartCoroutine (PlayAnimation (BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_TRIGGER, 
												BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE));
			} 
			else if (CanvasGroup != null) 
			{
				CanvasGroup.alpha = 0;
				CanvasGroup.FadeAlphaTo(1, .5f);
			}
		}

		public virtual void AnimateExit()
		{
			if (Animator != null && Animator.runtimeAnimatorController != null) 
			{
				StartCoroutine (PlayAnimation (BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_TRIGGER,
												BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE,
												() => gameObject.SetActive (false)));
			}
			else if(CanvasGroup != null)
			{	
				CanvasGroup.FadeAlphaTo(0, .5f, 0, () => gameObject.SetActive(false));
			}
			else
			{
				gameObject.SetActive(false);
			}
		}

		IEnumerator PlayAnimation(string animationTrigger, string animationStateName, Action onComplete = null)
		{
			Animator.SetTrigger(animationTrigger);

			yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName));
			while (Animator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName))
			{
				yield return new WaitForEndOfFrame();
			}

			if (onComplete != null) 
			{
				onComplete ();
			}
		}
	} 
}