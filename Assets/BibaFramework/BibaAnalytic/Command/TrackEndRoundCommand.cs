using strange.extensions.command.impl;

namespace BibaFramework.BibaAnalytic
{
	public class TrackEndRoundCommand : Command
	{
		[Inject]
		public IDeviceAnalyticService DeviceAnalyticService { get; set; }

		public override void Execute ()
		{
			DeviceAnalyticService.TrackEndRound();
		}
	}
}