using System;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;
using UnityEngine;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class PointsPopupMediator : Mediator
	{
		[Inject]
		public PointsPopupView PointsPopupView { get; set; }

		[Inject]
		public PointsGainedSignal PointsGainedSignal { get; set; }

		[Inject]
		public BibaSceneStack BibaSceneStack { get; set; }

		protected virtual bool ShouldShow {
			get {
				return !(BibaSceneStack.Count > 0 && BibaSceneStack.Peek () is IntroMenuState);
			}
		}

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
			if (ShouldShow) 
			{
				PointsPopupView.PointsGained(gainedPoints, totalPoints);
			}
		}
	}
}