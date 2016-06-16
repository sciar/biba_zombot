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
			if (BibaSessionModel.SessionInfo.VigorousTrackingStartTime != default(DateTime)) 
			{
				BibaSessionModel.SessionInfo.VigorousTrackingStartTime = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackModerateSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSessionModel.SessionInfo.VigorousTrackingStartTime != default(DateTime)) 
			{
				BibaSessionModel.SessionInfo.VigorousActivityTime += (float)(DateTime.UtcNow - BibaSessionModel.SessionInfo.VigorousTrackingStartTime).TotalSeconds;
				BibaSessionModel.SessionInfo.VigorousTrackingStartTime = default(DateTime);
			}
		}
	}
}