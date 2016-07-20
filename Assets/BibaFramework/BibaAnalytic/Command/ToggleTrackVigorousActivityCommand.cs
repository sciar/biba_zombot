using System;

namespace BibaFramework.BibaAnalytic
{
	public class ToggleTrackVigorousActivityCommand : BaseTrackActivityCommand
	{
		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackSedentarySignal { get; set; }

		[Inject]
		public ToggleTrackModerateActivitySignal ToggleTrackModerateSignal { get; set; }

		protected override void StartTracking ()
		{
			if (BibaProfile.BibaProfileSession.VScoreStart == default(DateTime)) 
			{
				BibaProfile.BibaProfileSession.VScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackModerateSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaProfile.BibaProfileSession.VScoreStart != default(DateTime)) 
			{
				BibaProfile.BibaProfileSession.SessionVScore += ((float)(DateTime.UtcNow - BibaProfile.BibaProfileSession.VScoreStart).TotalSeconds);
				BibaProfile.BibaProfileSession.VScoreStart = default(DateTime);

				DataService.Save ();
			}
		}
	}
}