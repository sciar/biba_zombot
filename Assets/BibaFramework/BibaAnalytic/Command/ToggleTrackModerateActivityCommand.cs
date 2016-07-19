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
			if (BibaAccount.SelectedProfile.BibaPlayerSession.MScoreStart == default(DateTime))
			{
				BibaAccount.SelectedProfile.BibaPlayerSession.MScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaAccount.SelectedProfile.BibaPlayerSession.MScoreStart != default(DateTime)) 
			{
				BibaAccount.SelectedProfile.MScore += ((float)(DateTime.UtcNow - BibaAccount.SelectedProfile.BibaPlayerSession.MScoreStart).TotalSeconds);
				BibaAccount.SelectedProfile.BibaPlayerSession.MScoreStart = default(DateTime);

				DataService.Save ();
			}
		}
	}
}