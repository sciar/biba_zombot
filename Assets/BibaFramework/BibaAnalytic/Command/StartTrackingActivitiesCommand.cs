using strange.extensions.command.impl;

namespace BibaFramework.BibaAnalytic
{
	public class StartTrackingActivitiesCommand : Command
	{
		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackLightActivitySignal { get; set; }

		public override void Execute ()
		{
			ToggleTrackLightActivitySignal.Dispatch (true);
		}
	}
}