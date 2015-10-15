using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class IntroContext : BaseBibaMenuContext 
    {
        public IntroContext (MonoBehaviour view) : base(view)
        {
        }
        
        public IntroContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<IntroView>().To<IntroMediator>();
        }
			
        protected override void BindCommands ()
        {         
        }

        protected override void BindSignals ()
        {
        }
    }
}