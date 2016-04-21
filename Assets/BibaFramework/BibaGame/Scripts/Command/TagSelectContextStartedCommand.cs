using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class TagSelectContextStartedCommand : Command
    {
        [Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

        public override void Execute ()
        {
			BibaSessionModel.TagEnabled = false;
        }
    }
}