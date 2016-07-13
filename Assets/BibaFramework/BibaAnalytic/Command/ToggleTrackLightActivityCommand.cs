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
			if(BibaSession.LScoreStart == default(DateTime))
			{
				BibaSession.LScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackModerateSignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSession.LScoreStart != default(DateTime)) 
			{
				BibaAccount.SetAllLScoreDelta ((float)(DateTime.UtcNow - BibaSession.LScoreStart).TotalSeconds);
				BibaSession.LScoreStart = default(DateTime);
			}
		}
	}
}