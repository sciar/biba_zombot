using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
    public class TryToSetHighScoreCommand : Command
    {
        [Inject]
        public int Score { get; set; }

        [Inject]
		public BibaDevice BibaDevice { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
			if (Score > BibaDevice.Highscore)
            {
				BibaDevice.Highscore = Score;
				DataService.Save();
            }
        }
    }
}