using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class EnableTagCommand : Command
    {
		[Inject]
		public bool Status { get; set; }

        [Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

        public override void Execute ()
        {
			BibaDeviceSession.TagEnabled = Status;
	    }
    }
}