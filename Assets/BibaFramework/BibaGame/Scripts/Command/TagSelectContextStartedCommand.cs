using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class TagSelectContextStartedCommand : Command
    {
        [Inject]
		public BibaGameModel BibaGameModel { get; set; }

		[Inject]
		public IDataService DataService { get; set; }

        public override void Execute ()
        {
			BibaGameModel.TagEnabled = false;
			DataService.WriteGameModel ();
        }
    }
}