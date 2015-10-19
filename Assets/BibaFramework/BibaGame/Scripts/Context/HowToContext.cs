using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class HowToContext : BaseBibaMenuContext 
    {
        public HowToContext (MonoBehaviour view) : base(view)
        {
        }
        
        public HowToContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<HowToView>().To<HowToMediator>();
        }
        
        protected override void BindCommands ()
        {   
        }
        
        protected override void BindSignals ()
        {
        }
    }
}