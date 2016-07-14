using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class EnableTagCommand : Command
    {
		[Inject]
		public bool Status { get; set; }

        [Inject]
		public BibaSession BibaSession { get; set; }

        public override void Execute ()
        {
			BibaSession.TagEnabled = Status;
	    }
    }
}