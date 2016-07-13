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
			if (BibaSession.MScoreStart == default(DateTime))
			{
				BibaSession.MScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSession.MScoreStart != default(DateTime)) 
			{
				BibaAccount.SetAllMScoreDelta ((float)(DateTime.UtcNow - BibaSession.MScoreStart).TotalSeconds);
				BibaSession.MScoreStart = default(DateTime);
			}
		}
	}
}