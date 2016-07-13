using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class DisableTagScanMenuConditionCommand : Command
    {
        [Inject]
		public BibaSession BibaSession { get; set; }

        public override void Execute ()
        {
			BibaSession.TagEnabled = false;
      	}
    }
}