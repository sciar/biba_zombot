using strange.extensions.command.impl;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public class LoadModelsCommand : Command
    {
        [Inject]
        public IDataService LoaderService { get; set; }

        public override void Execute ()
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