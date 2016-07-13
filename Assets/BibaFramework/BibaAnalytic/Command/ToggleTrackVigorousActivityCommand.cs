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
			if (BibaSession.VScoreStartTime == default(DateTime)) 
			{
				BibaSession.VScoreStartTime = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackModerateSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaSession.VScoreStartTime != default(DateTime)) 
			{
				BibaAccount.SetAllVScoreDelta((float)(DateTime.UtcNow - BibaSession.VScoreStartTime).TotalSeconds);
				BibaSession.VScoreStartTime = default(DateTime);
			}
		}
	}
}