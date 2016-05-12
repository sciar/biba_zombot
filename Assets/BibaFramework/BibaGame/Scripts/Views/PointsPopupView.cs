using System;
using System.Collections;
using BibaFramework.BibaMenu;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace BibaFramework.BibaGame
{
	public class PointsPopupView : View
	{
		private const string ACTIVE = "Active";
		private const string START = "Start";
		private const string IDLE = "Idle";

		public Text PointsLabel;
		public Animator Anim;

		public void PointsGained(int totalPoints, int pointsGained)
		{
			StartCoroutine(PointsGainedAnimation(totalPoints, pointsGained));
		}

		IEnumerator PointsGainedAnimation(int totalPoints, int pointsGained)
		{
			var startpoints = totalPoints - pointsGained;
			PointsLabel.text = startpoints.ToString ();

			yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(START));
			Anim.SetTrigger (BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE);

			yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE));
			while (Anim.GetCurrentAnimatorStateInfo (0).IsName (BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE)) 
			{
				yield return new WaitForEndOfFrame();
			}

			for(int i = 1; i <= pointsGained; i++)
			{
				yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(ACTIVE));
				while(Anim.GetCurrentAnimatorStateInfo (0).IsName (ACTIVE))
				{
					yield return new WaitForEndOfFrame();
				}
					
				PointsLabel.text = (startpoints + i).ToString ();

				yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(IDLE));
				Anim.SetTrigger(MenuStateTrigger.Next);
			}

			Anim.SetTrigger (BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE);
			yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE));
			while (Anim.GetCurrentAnimatorStateInfo (0).IsName (BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE)) 
			{
				yield return new WaitForEndOfFrame();
			}

			PointsLabel.text = totalPoints.ToString ();
		}
	}
}