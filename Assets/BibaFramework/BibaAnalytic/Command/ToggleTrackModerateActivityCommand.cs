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
			if (BibaSessionModel.SessionInfo.ModerateTrackingStartTime != default(DateTime))
			{
				BibaSessionModel.SessionInfo.ModerateTrackingStartTime = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSessionModel.SessionInfo.ModerateTrackingStartTime != default(DateTime)) 
			{
				BibaSessionModel.SessionInfo.ModerateActivityTime += (float)(DateTime.UtcNow - BibaSessionModel.SessionInfo.ModerateTrackingStartTime).TotalSeconds;
				BibaSessionModel.SessionInfo.ModerateTrackingStartTime = default(DateTime);
			}
		}
	}
}