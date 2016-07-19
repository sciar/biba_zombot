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
			if (BibaAccount.SelectedProfile.BibaPlayerSession.VScoreStart == default(DateTime)) 
			{
				BibaAccount.SelectedProfile.BibaPlayerSession.VScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackSedentarySignal.Dispatch (false);
			ToggleTrackModerateSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaAccount.SelectedProfile.BibaPlayerSession.VScoreStart != default(DateTime)) 
			{
				BibaAccount.SelectedProfile.VScore += ((float)(DateTime.UtcNow - BibaAccount.SelectedProfile.BibaPlayerSession.VScoreStart).TotalSeconds);
				BibaAccount.SelectedProfile.BibaPlayerSession.VScoreStart = default(DateTime);

				DataService.Save ();
			}
		}
	}
}