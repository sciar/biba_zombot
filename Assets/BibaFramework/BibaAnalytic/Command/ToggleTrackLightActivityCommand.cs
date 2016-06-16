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
			if(BibaSessionModel.RoundInfo.LightTrackingStartTime != default(DateTime))
			{
				BibaSessionModel.RoundInfo.LightTrackingStartTime = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackModerateSignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSessionModel.RoundInfo.LightTrackingStartTime != default(DateTime)) 
			{
				BibaSessionModel.RoundInfo.LightActivityTime += (float)(DateTime.UtcNow - BibaSessionModel.RoundInfo.LightTrackingStartTime).TotalSeconds;
				BibaSessionModel.RoundInfo.LightTrackingStartTime = default(DateTime);
			}
		}
	}
}