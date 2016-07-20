using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class SettingsContext : BaseBibaMenuContext 
    {
        public SettingsContext (MonoBehaviour view) : base(view)
        {
        }
        
        public SettingsContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }
        
        protected override void BindModels ()
        {
        }
        
        protected override void BindServices ()
        {
        }
        
        protected override void BindViews ()
        { 
            mediationBinder.Bind<SettingsView>().To<SettingsMediator>();
			mediationBinder.Bind<ResetAlertsView> ().To<ResetAlertsMediator> ();
			mediationBinder.Bind<SetLanguageToggleView> ().To<SetLanguageToggleMediator> ();
        }
        
        protected override void BindCommands ()
        {   
            commandBinder.Bind<EnableHelpBubblesSignal>().To<EnableHelpBubblesCommand>();
            commandBinder.Bind<EnableHowToSignal>().To<EnableHowToCommand>();
			commandBinder.Bind<ResetGameModelSignal>().To<ResetGameModelCommand>().To<SetDeviceModelCommand>().InSequence();
			commandBinder.Bind<SetLanguageOverwriteSignal>().To<SetLanguageOverwriteCommand>();
        }
        
        protected override void BindSignals ()
        {
        }
    }
}