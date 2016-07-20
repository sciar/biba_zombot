using System;

namespace BibaFramework.BibaAnalytic
{
	public class ToggleTrackLightActivityCommand : BaseTrackActivityCommand
	{
		[Inject]
		public ToggleTrackModerateActivitySignal ToggleTrackModerateSignal { get; set; }

		[Inject]
		public ToggleTrackVigorousActivitySignal ToggleTrackVigorousSignal { get; set; }

		protected override void StartTracking ()
		{
			if(BibaProfile.BibaProfileSession.LScoreStart == default(DateTime))
			{
				BibaProfile.BibaProfileSession.LScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackModerateSignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaProfile.BibaProfileSession.LScoreStart != default(DateTime)) 
			{
				BibaProfile.BibaProfileSession.SessionLScore += ((float)(DateTime.UtcNow - BibaProfile.BibaProfileSession.LScoreStart).TotalSeconds);
				BibaProfile.BibaProfileSession.LScoreStart = default(DateTime);

				DataService.Save ();
			}
		}
	}
}