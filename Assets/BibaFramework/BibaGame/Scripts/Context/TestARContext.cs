using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;
using BibaFramework.BibaTag;

namespace BibaFramework.BibaGame
{
    public class TestARContext : BaseBibaMenuContext 
    {
        public TestARContext (MonoBehaviour view) : base(view)
        {
        }
        
        public TestARContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }
        
        protected override void BindModels ()
        {
        }
        
        protected override void BindServices ()
        {
            injectionBinder.Bind<IBibaTagService>().To<VuforiaTagService>();
        }
        
        protected override void BindViews ()
        { 
            mediationBinder.Bind<TestARView>().To<TestARMediator>();
        }
        
        protected override void BindCommands ()
        {   
        }
        
        protected override void BindSignals ()
        {
        }
    }
}