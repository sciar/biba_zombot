using strange.extensions.command.impl;
using BibaFramework.BibaMenu;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
	public class EnableTagCommand : Command
    {
		[Inject]
		public bool Status { get; set; }

        [Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public IDeviceAnalyticService DeviceAnalyticService { get; set; }

        public override void Execute ()
        {
			BibaDeviceSession.TagEnabled = Status;
			DeviceAnalyticService.TrackTagEnabled ();
	    }
    }
}