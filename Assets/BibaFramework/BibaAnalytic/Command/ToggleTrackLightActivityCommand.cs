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
			if(BibaSession.LScoreStartTime == default(DateTime))
			{
				BibaSession.LScoreStartTime = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackModerateSignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSession.LScoreStartTime != default(DateTime)) 
			{
				BibaAccount.SetAllLScoreDelta ((float)(DateTime.UtcNow - BibaSession.LScoreStartTime).TotalSeconds);
				BibaSession.LScoreStartTime = default(DateTime);
			}
		}
	}
}