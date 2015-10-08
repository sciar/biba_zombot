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
            var gameModel = LoaderService.ReadGameModel();
            injectionBinder.Unbind<BibaGameModel>();
            injectionBinder.Bind<BibaGameModel>().To(gameModel).ToSingleton().CrossContext();
        }
    } 
}