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

		public void PointsGained(int gainedPoints, int totalPoints)
		{
			StopAllCoroutines ();
			StartCoroutine(PointsGainedAnimation(gainedPoints, totalPoints));
		}

		IEnumerator PointsGainedAnimation(int gainedPoints, int totalPoints)
		{
			Anim.SetTrigger (START);
			yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(START));
			Anim.SetTrigger (BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE);

			yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE));
			while (Anim.GetCurrentAnimatorStateInfo (0).IsName (BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE)) 
			{
				yield return new WaitForEndOfFrame();
			}
				
			var startPoints = totalPoints - gainedPoints;
			var displayedPoints = int.Parse (PointsLabel.text);
			var currentPoints = displayedPoints > startPoints ? displayedPoints : startPoints;
			while (currentPoints < totalPoints) 
			{
				yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(ACTIVE));
				while(Anim.GetCurrentAnimatorStateInfo (0).IsName (ACTIVE))
				{
					yield return new WaitForEndOfFrame();
				}

				currentPoints++;
				PointsLabel.text = currentPoints.ToString ();

				yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(IDLE));
				Anim.SetTrigger(MenuStateTrigger.Next);
			}
			PointsLabel.text = totalPoints.ToString ();

			Anim.SetTrigger (BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE);
			yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE));
			while (Anim.GetCurrentAnimatorStateInfo (0).IsName (BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE)) 
			{
				yield return new WaitForEndOfFrame();
			}
		}
	}
}