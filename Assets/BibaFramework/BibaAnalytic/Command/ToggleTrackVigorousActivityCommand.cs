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
			if (BibaSessionModel.RoundInfo.VigorousTrackingStartTime != default(DateTime)) 
			{
				BibaSessionModel.RoundInfo.VigorousTrackingStartTime = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackModerateSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSessionModel.RoundInfo.VigorousTrackingStartTime != default(DateTime)) 
			{
				BibaSessionModel.RoundInfo.VigorousActivityTime += (float)(DateTime.UtcNow - BibaSessionModel.RoundInfo.VigorousTrackingStartTime).TotalSeconds;
				BibaSessionModel.RoundInfo.VigorousTrackingStartTime = default(DateTime);
			}
		}
	}
}