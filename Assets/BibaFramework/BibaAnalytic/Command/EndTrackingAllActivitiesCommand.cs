using strange.extensions.command.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaAnalytic
{
	public class EndTrackingAllActivitiesCommand : Command
	{
		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackLightActivitySignal { get; set; }

		[Inject]
		public ToggleTrackModerateActivitySignal ToggleTrackModerateActivitySignal { get; set; }

		[Inject]
		public ToggleTrackVigorousActivitySignal ToggleTrackVigorousActivitySignal { get; set; }

		[Inject]
		public IAnalyticService AnalyticService { get; set; }

		[Inject]
		public BibaSession BibaSessionModel { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

		public override void Execute ()
		{
			ToggleTrackLightActivitySignal.Dispatch (false);
			ToggleTrackModerateActivitySignal.Dispatch (false);
			ToggleTrackVigorousActivitySignal.Dispatch (false);

			AnalyticService.TrackActivites ();
			BibaAccount.ResetLMVScores ();
		}
	}
}