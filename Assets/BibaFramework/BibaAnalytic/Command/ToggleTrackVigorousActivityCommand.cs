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
			if (BibaProfile.BibaPlayerSession.VScoreStart == default(DateTime)) 
			{
				BibaProfile.BibaPlayerSession.VScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackModerateSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaProfile.BibaPlayerSession.VScoreStart != default(DateTime)) 
			{
				BibaProfile.VScore += ((float)(DateTime.UtcNow - BibaProfile.BibaPlayerSession.VScoreStart).TotalSeconds);
				BibaProfile.BibaPlayerSession.VScoreStart = default(DateTime);

				DataService.Save ();
			}
		}
	}
}