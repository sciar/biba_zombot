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
			var systemModel = LoaderService.LoadSystemModel ();
			injectionBinder.Unbind<BibaSystem>();
			injectionBinder.Bind<BibaSystem>().To(systemModel).ToSingleton().CrossContext();

			var accountModel = LoaderService.LoadAccountModel ();
			injectionBinder.Unbind<BibaAccount>();
			injectionBinder.Bind<BibaAccount>().To(accountModel).ToSingleton().CrossContext();
	     }
    } 
}