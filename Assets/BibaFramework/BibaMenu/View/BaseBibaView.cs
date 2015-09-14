using System;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
	public abstract class BaseBibaView : View
	{
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
            yield return new WaitForSeconds(3.0f);
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
            yield return new WaitForSeconds(3.0f);
        }
	}
}