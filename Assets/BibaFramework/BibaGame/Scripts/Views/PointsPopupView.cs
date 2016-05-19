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
		protected const string ACTIVE = "Active";
		protected const string START = "Start";

		public Text PointsLabel;
		public Animator Anim;
		public GameObject PointGainedPrefab;

		public void PointsGained(int gainedPoints, int totalPoints)
		{
			StopAllCoroutines ();

			_totalPoints = totalPoints;
			StartCoroutine(PointsGainedAnimation(gainedPoints));
		}

		private int _currentPoints;
		private int _totalPoints;
		protected virtual IEnumerator PointsGainedAnimation(int gainedPoints)
		{
			Anim.SetTrigger (START);
			yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(START));

			var startPoints = _totalPoints - gainedPoints;
			var displayedPoints = int.Parse (PointsLabel.text);

			_currentPoints = displayedPoints > startPoints ? displayedPoints : startPoints;
			PointsLabel.text = _currentPoints.ToString ();

			//Menu Entry
			Anim.SetTrigger (BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE);
			yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE));
			while (Anim.GetCurrentAnimatorStateInfo (0).IsName (BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_STATE)) 
			{
				yield return new WaitForEndOfFrame();
			}

			//Gain Point
			yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).IsName(ACTIVE));
			for (int i = _currentPoints; i < _totalPoints; i++) 
			{
				var go = Instantiate (PointGainedPrefab).GetComponent<RectTransform>();
				go.SetParent(transform);
				go.sizeDelta = PointsLabel.rectTransform.sizeDelta;
				go.anchoredPosition = PointsLabel.rectTransform.anchoredPosition;
				go.GetComponent<PointsGainedLabel> ().PointsGainedSignal.AddOnce(IncrementCoin);

				yield return new WaitForSeconds (0.1f);
			}
		}

		public void IncrementCoin()
		{
			_currentPoints++;
			PointsLabel.text = _currentPoints.ToString ();

			if (_currentPoints >= _totalPoints) 
			{
				StopAllCoroutines ();
				AnimateExit ();
			}
		}

		void AnimateExit()
		{
			PointsLabel.text = _totalPoints.ToString ();
			Anim.SetTrigger (BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_STATE);
		}
	}
}