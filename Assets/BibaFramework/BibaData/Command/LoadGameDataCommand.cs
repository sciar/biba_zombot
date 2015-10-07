using strange.extensions.command.impl;
using BibaFramework.BibaGame;
using UnityEngine;

namespace BibaFramework.BibaData
{
    public class LoadGameDataCommand : Command
    {
        [Inject]
        public IDataService LoaderService { get; set; }

        public override void Execute ()
        {
            var loadedGameModel = LoaderService.ReadFromDisk<BibaGameModel>(BibaDataConstants.GAME_MODEL_DATA_PATH);
            injectionBinder.Bind<BibaGameModel>().To(loadedGameModel);
        }
    }
}