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
			if(BibaProfile.BibaPlayerSession.LScoreStart == default(DateTime))
			{
				BibaProfile.BibaPlayerSession.LScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackModerateSignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaProfile.BibaPlayerSession.LScoreStart != default(DateTime)) 
			{
				BibaProfile.LScore += ((float)(DateTime.UtcNow - BibaProfile.BibaPlayerSession.LScoreStart).TotalSeconds);
				BibaProfile.BibaPlayerSession.LScoreStart = default(DateTime);

				DataService.Save ();
			}
		}
	}
}