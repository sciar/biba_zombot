using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class InactiveContext : BaseBibaMenuContext 
    {
        public InactiveContext (MonoBehaviour view) : base(view)
        {
        }
        
        public InactiveContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<InactiveView>().To<InactiveMediator>();
        }

        protected override void BindCommands ()
        {    
            commandBinder.Bind<StartSignal>().To<DisableInactiveMenuConditionCommand>();
			commandBinder.Bind<StartNewSessionSignal> ().To<StartNewSessionCommand> ();
        }

        protected override void BindSignals ()
        {
        }
    }
}