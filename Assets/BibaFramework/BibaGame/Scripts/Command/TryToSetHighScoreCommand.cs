using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
    public class TryToSetHighScoreCommand : Command
    {
        [Inject]
        public int Score { get; set; }

        [Inject]
		public BibaSystem BibaSystem { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
			if (Score > BibaSystem.Highscore)
            {
				BibaSystem.Highscore = Score;
				DataService.Save();
            }
        }
    }
}