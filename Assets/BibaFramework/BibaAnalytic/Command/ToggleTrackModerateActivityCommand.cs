using System;

namespace BibaFramework.BibaAnalytic
{
	public class ToggleTrackModerateActivityCommand : BaseTrackActivityCommand
	{
		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackSedentarySignal { get; set; }

		[Inject]
		public ToggleTrackVigorousActivitySignal ToggleTrackVigorousSignal { get; set; }

		protected override void StartTracking ()
		{
			if (BibaProfile.BibaProfileSession.MScoreStart == default(DateTime))
			{
				BibaProfile.BibaProfileSession.MScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaProfile.BibaProfileSession.MScoreStart != default(DateTime)) 
			{
				BibaProfile.BibaProfileSession.SessionMScore += ((float)(DateTime.UtcNow - BibaProfile.BibaProfileSession.MScoreStart).TotalSeconds);
				BibaProfile.BibaProfileSession.MScoreStart = default(DateTime);

				DataService.Save ();
			}
		}
	}
}