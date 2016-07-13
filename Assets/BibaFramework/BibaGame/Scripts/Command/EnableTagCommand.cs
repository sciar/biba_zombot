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
		public BibaSession BibaSession { get; set; }

        [Inject]
        public IAnalyticService BibaAnalyticService { get; set; } 


        public override void Execute ()
        {
			BibaSession.TagEnabled = Status;
            BibaAnalyticService.TrackTagEnabled(Status);
	    }
    }
}