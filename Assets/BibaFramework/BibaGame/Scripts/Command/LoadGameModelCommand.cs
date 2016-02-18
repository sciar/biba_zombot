using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
    public class LoadGameModelCommand : Command
    {
        [Inject]
        public IDataService LoaderService { get; set; }

        public override void Execute ()
        {
            LoadGameModel();
        }

        void LoadGameModel()
        {
            var gameModel = LoaderService.ReadGameModel();
            injectionBinder.Unbind<BibaGameModel>();
            injectionBinder.Bind<BibaGameModel>().To(gameModel).ToSingleton().CrossContext();
        }
    } 
}