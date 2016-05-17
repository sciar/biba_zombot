using UnityEngine;
using System.Collections;
using BibaFramework.BibaGame;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace BibaFramework.BibaGame
{
	public class ResetAlertsView : View 
	{
		private const string ENABLE = "Enabled";

		public Animator alertAnimator;

		public Signal ResetPlayerSignal = new Signal();

		public void EraseModel() 
		{
			ResetPlayerSignal.Dispatch ();
		}
		
		public void OpenPrompt() 
		{
			alertAnimator.SetBool (ENABLE, true);
		}
		
		public void ClosePrompt(bool value) 
		{
			if (value) 
			{
				EraseModel();
			}

			alertAnimator.SetBool (ENABLE, false);
		}
	}
}