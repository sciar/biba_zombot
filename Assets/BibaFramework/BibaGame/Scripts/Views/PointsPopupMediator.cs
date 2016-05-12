using System;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaGame
{
	public class PointsPopupMediator : Mediator
	{
		[Inject]
		public PointsPopupView PointsPopupView { get; set; }

		[Inject]
		public PointsGainedSignal PointsGainedSignal { get; set; }

		[Inject]
		public BibaGameModel BibaGameModel { get; set; }

		public override void OnRegister ()
		{
			PointsPopupView.PointsLabel.text = BibaGameModel.Points.ToString();
			PointsGainedSignal.AddListener (PlayPointsAnimation);
		}

		public override void OnRemove ()
		{
			PointsGainedSignal.RemoveListener (PlayPointsAnimation);
		}

		void PlayPointsAnimation(int pointsGained)
		{
			PointsPopupView.PointsGained(BibaGameModel.Points, pointsGained);
		}
	}
}