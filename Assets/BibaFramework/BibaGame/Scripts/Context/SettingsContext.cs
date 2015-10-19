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
        }
        
        protected override void BindCommands ()
        {   
            commandBinder.Bind<EnableHelpBubblesSignal>().To<EnableHelpBubblesCommand>();
            commandBinder.Bind<EnableHowToSignal>().To<EnableHowToCommand>();
        }
        
        protected override void BindSignals ()
        {
        }
    }
}