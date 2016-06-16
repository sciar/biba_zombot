using strange.extensions.command.impl;

namespace BibaFramework.BibaAnalytic
{
	public class EndTrackingActivityCommand : Command
	{
		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackLightActivitySignal { get; set; }

		[Inject]
		public ToggleTrackModerateActivitySignal ToggleTrackModerateActivitySignal { get; set; }

		[Inject]
		public ToggleTrackVigorousActivitySignal ToggleTrackVigorousActivitySignal { get; set; }

		public override void Execute ()
		{
			ToggleTrackLightActivitySignal.Dispatch (false);
			ToggleTrackModerateActivitySignal.Dispatch (false);
			ToggleTrackVigorousActivitySignal.Dispatch (false);
		}
	}
}