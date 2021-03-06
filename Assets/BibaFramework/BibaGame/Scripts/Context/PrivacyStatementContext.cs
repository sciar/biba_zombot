using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class PrivacyStatementContext : BaseBibaMenuContext 
    {
        public PrivacyStatementContext (MonoBehaviour view) : base(view)
        {
        }
        
        public PrivacyStatementContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<PrivacyStatementView>().To<PrivacyStatementMediator>();
        }
        
        protected override void BindCommands ()
		{   
            commandBinder.Bind<EndSignal>().To<EnablePrivacyStatementCommand>().To<LogLocationInfoCommand>();
        }
        
        protected override void BindSignals ()
        {
        }
    }
}