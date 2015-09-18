using System;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    [RequireComponent(typeof(Animator))]
	public abstract class BaseBibaView : View
	{
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
            if (anim.runtimeAnimatorController != null && anim.HasState(0, Animator.StringToHash(BibaConstants.BIBA_MENU_ENTRY_ANIMATION_STATE)))
            {
                anim.SetTrigger(BibaConstants.BIBA_MENU_ENTRY_ANIMATION_TRIGGER);

                while (anim.GetCurrentAnimatorStateInfo(0).IsName(BibaConstants.BIBA_MENU_ENTRY_ANIMATION_STATE))
                {
                    yield return null;
                }
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
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
            if (anim.runtimeAnimatorController != null && anim.HasState(0, Animator.StringToHash(BibaConstants.BIBA_MENU_EXIT_ANIMATION_STATE)))
            {
                anim.SetTrigger(BibaConstants.BIBA_MENU_EXIT_ANIMATION_TRIGGER);
                
                while (anim.GetCurrentAnimatorStateInfo(0).IsName(BibaConstants.BIBA_MENU_EXIT_ANIMATION_STATE))
                {
                    yield return null;
                }
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
	}
}