using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class BibaPresentContext : BaseBibaMenuContext 
    {
        public BibaPresentContext (MonoBehaviour view) : base(view)
        {
        }
        
        public BibaPresentContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<BibaPresentView>().To<BibaPresentMediator>();
        }
        
        protected override void BindCommands ()
        {   
			commandBinder.Bind<StartSignal>().To<SetBibaPresentShownCommand>();
        }
        
        protected override void BindSignals ()
        {
        }
    }
}