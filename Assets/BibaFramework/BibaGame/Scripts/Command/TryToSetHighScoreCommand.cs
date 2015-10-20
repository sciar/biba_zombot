using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
    public class TryToSetHighScoreCommand : Command
    {
        [Inject]
        public float Score { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
            if (Score > BibaGameModel.HighScore)
            {
                BibaGameModel.HighScore = Score;
                DataService.WriteGameModel();
            }
        }
    }
}