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
			if (BibaSessionModel.RoundInfo.ModerateTrackingStartTime != default(DateTime))
			{
				BibaSessionModel.RoundInfo.ModerateTrackingStartTime = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSessionModel.RoundInfo.ModerateTrackingStartTime != default(DateTime)) 
			{
				BibaSessionModel.RoundInfo.ModerateActivityTime += (float)(DateTime.UtcNow - BibaSessionModel.RoundInfo.ModerateTrackingStartTime).TotalSeconds;
				BibaSessionModel.RoundInfo.ModerateTrackingStartTime = default(DateTime);
			}
		}
	}
}