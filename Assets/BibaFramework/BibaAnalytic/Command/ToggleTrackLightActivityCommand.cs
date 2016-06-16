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
			if(BibaSessionModel.SessionInfo.LightTrackingStartTime == default(DateTime))
			{
				BibaSessionModel.SessionInfo.LightTrackingStartTime = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackModerateSignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSessionModel.SessionInfo.LightTrackingStartTime != default(DateTime)) 
			{
				BibaSessionModel.SessionInfo.LightActivityTime += (float)(DateTime.UtcNow - BibaSessionModel.SessionInfo.LightTrackingStartTime).TotalSeconds;
				BibaSessionModel.SessionInfo.LightTrackingStartTime = default(DateTime);
			}
		}
	}
}