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
			if (BibaProfile.BibaPlayerSession.MScoreStart == default(DateTime))
			{
				BibaProfile.BibaPlayerSession.MScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaProfile.BibaPlayerSession.MScoreStart != default(DateTime)) 
			{
				BibaProfile.MScore += ((float)(DateTime.UtcNow - BibaProfile.BibaPlayerSession.MScoreStart).TotalSeconds);
				BibaProfile.BibaPlayerSession.MScoreStart = default(DateTime);

				DataService.Save ();
			}
		}
	}
}