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
		public BibaSessionModel BibaSessionModel { get; set; }

        [Inject]
        public IAnalyticService BibaAnalyticService { get; set; } 

        public override void Execute ()
        {
			BibaSessionModel.TagEnabled = Status;
            BibaAnalyticService.TrackSatelliteTagEnabled(Status);
        }
    }
}