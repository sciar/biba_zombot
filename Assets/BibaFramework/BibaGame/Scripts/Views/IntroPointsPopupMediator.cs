using System;

namespace BibaFramework.BibaGame
{
	public class IntroPointsPopupMediator : PointsPopupMediator
	{
		[Inject]
		public BibaAccount BibaAccount  { get; set; }

		public override void OnRegister ()
		{
			base.OnRegister ();
			PointsPopupView.PointsLabel.text = BibaAccount.TotalPoints.ToString ();
		}

		protected override bool ShouldShow {
			get {
				return !base.ShouldShow;
			}
		}
	}
}