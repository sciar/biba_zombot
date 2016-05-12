using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class StartContext : BaseBibaMenuContext 
    {
        public StartContext (MonoBehaviour view) : base(view)
        {
        }
        
        public StartContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<StartView>().To<StartMediator>();
        }

        protected override void BindCommands ()
        {    
			commandBinder.Bind<StartSignal> ().To<CheckForFirstStartPointsEventCommand> ();
        }

        protected override void BindSignals ()
        {
        }
    }
}