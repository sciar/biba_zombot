using strange.extensions.command.impl;

namespace BibaFramework.BibaAnalytic
{
	public class StartTrackingActivityCommand : Command
	{
		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackLightActivitySignal { get; set; }

		public override void Execute ()
		{
			ToggleTrackLightActivitySignal.Dispatch (true);
		}
	}
}