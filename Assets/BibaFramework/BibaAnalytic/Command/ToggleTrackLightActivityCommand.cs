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
			if(BibaAccount.SelectedProfile.BibaPlayerSession.LScoreStart == default(DateTime))
			{
				BibaAccount.SelectedProfile.BibaPlayerSession.LScoreStart = DateTime.UtcNow;
			}
		}

		protected override void TurnOffOtherTrackingSignals ()
		{
			ToggleTrackModerateSignal.Dispatch (false);
			ToggleTrackVigorousSignal.Dispatch (false);
		}

		protected override void AddActivityTime ()
		{
			if (BibaAccount.SelectedProfile.BibaPlayerSession.LScoreStart != default(DateTime)) 
			{
				BibaAccount.SelectedProfile.LScore += ((float)(DateTime.UtcNow - BibaAccount.SelectedProfile.BibaPlayerSession.LScoreStart).TotalSeconds);
				BibaAccount.SelectedProfile.BibaPlayerSession.LScoreStart = default(DateTime);

				DataService.Save ();
			}
		}
	}
}