using System;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaAnalytic
{
	public class ToggleTrackModerateActivityCommand : BaseTrackActivityCommand
	{
		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackSedentarySignal { get; set; }

		[Inject]
		public ToggleTrackVigorousActivitySignal ToggleTrackVigorousSignal { get; set; }

		protected override LMVSession LMVSession {
			get {
				return BibaProfile.BibaProfileSession.LMVSessionDict [LMVScoreType.moderate_score];
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}
	}
}