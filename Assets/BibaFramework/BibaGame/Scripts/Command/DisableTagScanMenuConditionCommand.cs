using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class DisableTagScanMenuConditionCommand : Command
    {
        [Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

        public override void Execute ()
        {
			BibaDeviceSession.TagEnabled = false;
      	}
    }
}