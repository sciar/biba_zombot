using strange.extensions.command.impl;
using System.Linq;

namespace BibaFramework.BibaGame
{
	public class SetModelsCommand : Command
    {
        [Inject]
        public IDataService LoaderService { get; set; }

        public override void Execute ()
        {
            var systemModel = LoaderService.LoadSystemModel ();
			injectionBinder.Unbind<BibaDevice>();
			injectionBinder.Bind<BibaDevice>().To(systemModel).ToSingleton().CrossContext();

			var accountModel = LoaderService.LoadAccountModel ();
			injectionBinder.Unbind<BibaAccount>();
			injectionBinder.Bind<BibaAccount>().To(accountModel).ToSingleton().CrossContext();
		}
    } 
}