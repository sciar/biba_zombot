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
			if (BibaSession.VScoreStart == default(DateTime)) 
			{
				BibaSession.VScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackModerateSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSession.VScoreStart != default(DateTime)) 
			{
				BibaAccount.SetAllVScoreDelta((float)(DateTime.UtcNow - BibaSession.VScoreStart).TotalSeconds);
				BibaSession.VScoreStart = default(DateTime);
			}
		}
	}
}