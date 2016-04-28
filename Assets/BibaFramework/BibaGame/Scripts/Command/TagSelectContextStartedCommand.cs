using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class TagSelectContextStartedCommand : Command
    {
        [Inject]
		public BibaGameModel BibaGameModel { get; set; }

        public override void Execute ()
        {
			BibaGameModel.TagEnabled = false;
        }
    }
}