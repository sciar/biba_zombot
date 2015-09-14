using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaGame
{
    public class MainContext : BaseBibaMenuContext 
    {
        public MainContext (MonoBehaviour view) : base(view)
        {
        }
        
        public MainContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<MainView>().To<MainMediator>();
        }

        protected override void BindCommands ()
        {   
        }

        protected override void BindSignals ()
        {
        }
    }
}