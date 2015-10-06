using strange.extensions.command.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaData
{
    public class LoadGameDataCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService LoaderService { get; set; }
       
        public override void Execute ()
        {
            BibaGameModel = LoaderService.ReadFromDisk<BibaGameModel>(BibaDataConstants.GAME_MODEL_DATA_PATH);
        }
    }
}

