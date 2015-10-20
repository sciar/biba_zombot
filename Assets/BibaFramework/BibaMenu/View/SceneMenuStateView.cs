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
            yield return StartCoroutine(AnimateMenuEntry());
            onComplete();
        }

        protected virtual IEnumerator AnimateMenuEntry()
        {
            if (anim.runtimeAnimatorController != null && anim.HasState(0, Animator.StringToHash(BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE)))
            {
                anim.SetTrigger(BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_TRIGGER);

                //Unity needs one frame to transition to the animation state
                yield return null;

                while (anim.GetCurrentAnimatorStateInfo(0).IsName(BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE))
                {
                    yield return null;
                }
            }
        }

        public void StartExitAnimation(Action onComplete)
        {
            StartCoroutine(PlayExitAnimation(onComplete));
        }
        
        IEnumerator PlayExitAnimation(Action onComplete)
        {
            yield return StartCoroutine(AnimateMenuExit());
            onComplete();
        }

        protected virtual IEnumerator AnimateMenuExit()
        {
            if (anim.runtimeAnimatorController != null && anim.HasState(0, Animator.StringToHash(BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE)))
            {
                anim.SetTrigger(BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_TRIGGER);

                //Unity needs one frame to transition to the animation state
                yield return null;

                while (anim.GetCurrentAnimatorStateInfo(0).IsName(BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE))
                {
                    yield return null;
                }
            }
        }
	}
}