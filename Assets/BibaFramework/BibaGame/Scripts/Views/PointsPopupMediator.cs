using System;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class PointsPopupMediator : Mediator
	{
		[Inject]
		public PointsPopupView PointsPopupView { get; set; }

		[Inject]
		public PointsGainedSignal PointsGainedSignal { get; set; }

		public override void OnRegister ()
		{
			PointsGainedSignal.AddListener (PlayPointsAnimation);
		}

		public override void OnRemove ()
		{
			PointsGainedSignal.RemoveListener (PlayPointsAnimation);
		}

		void PlayPointsAnimation(int gainedPoints, int totalPoints)
		{	
			PointsPopupView.PointsGained(gainedPoints, totalPoints);
		}
	}
}