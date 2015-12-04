using strange.extensions.mediation.impl;
using UnityEngine;
using System;

namespace BibaFramework.BibaMenu
{
    public abstract class BaseObjectMenuStateView : View 
	{
		public CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }

		public virtual void AnimateEntry()
		{
			gameObject.SetActive(true);
			if(CanvasGroup != null)
			{
				CanvasGroup.alpha = 0;
				CanvasGroup.FadeAlphaTo(1, .5f);
			}
		}

		public virtual void AnimateExit()
		{
			if(CanvasGroup != null)
			{	
				CanvasGroup.FadeAlphaTo(0, .5f, 0, () => gameObject.SetActive(false));
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	} 
}