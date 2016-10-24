using System;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    [RequireComponent(typeof(Animator))]
	public abstract class SceneMenuStateView : View, IAudioView
	{
        public AudioServices AudioServices { get; set; }

        private Animator _anim;
        private Animator anim {
            get {
                if(_anim == null)
                {
                    _anim = GetComponent<Animator>();
                }
                return _anim;
            }
        }

        public void StartEntryAnimation(Action onComplete)
        {
            StartCoroutine(PlayEntryAnimation(onComplete));
        }

        IEnumerator PlayEntryAnimation(Action onComplete)
        {
			yield return StartCoroutine(PlayAnimation(BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_TRIGGER, BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE));
            onComplete();
        }

        public void StartExitAnimation(Action onComplete)
        {
            StartCoroutine(PlayExitAnimation(onComplete));
        }
        
        IEnumerator PlayExitAnimation(Action onComplete)
        {
			yield return StartCoroutine(PlayAnimation(BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_TRIGGER, BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE));
            onComplete();
        }

		IEnumerator PlayAnimation(string animationTrigger, string animationStateName)
		{
			if (anim.runtimeAnimatorController != null && anim.HasState(0, Animator.StringToHash(animationStateName)))
			{
				anim.SetTrigger(animationTrigger);
				yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(animationStateName));
				while (anim.GetCurrentAnimatorStateInfo(0).IsName(animationStateName))
				{
					yield return new WaitForEndOfFrame();
				}
			}
		}
	}
} 