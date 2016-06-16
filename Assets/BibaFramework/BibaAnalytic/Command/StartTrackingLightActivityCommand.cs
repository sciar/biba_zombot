using strange.extensions.command.impl;

namespace BibaFramework.BibaAnalytic
{
	public class StartTrackingLightActivityCommand : Command
	{
		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackLightActivitySignal { get; set; }

		public override void Execute ()
		{
			ToggleTrackLightActivitySignal.Dispatch (true);
		}
	}
}