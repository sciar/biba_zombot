using System;

namespace BibaFramework.BibaGame
{
	public class IntroPointsPopupMediator : PointsPopupMediator
	{
		[Inject]
		public BibaGameModel BibaGameModel  { get; set; }

		public override void OnRegister ()
		{
			base.OnRegister ();
			PointsPopupView.PointsLabel.text = BibaGameModel.Points.ToString ();
		}

		protected override bool ShouldShow {
			get {
				return !base.ShouldShow;
			}
		}
	}
}