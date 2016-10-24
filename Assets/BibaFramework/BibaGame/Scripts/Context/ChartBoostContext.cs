using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class ChartBoostContext : BaseBibaMenuContext 
    {
        public ChartBoostContext (MonoBehaviour view) : base(view)
        {
        }
        
        public ChartBoostContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<ChartBoostView>().To<ChartBoostMediator>();
        }
        
        protected override void BindCommands ()
        {
            commandBinder.Bind<StartSignal>().To<LogChartBoostDisplayTimeCommand>().To<LoadInterstitialCommand>();
        }
        
        protected override void BindSignals ()
        {
        }
    }
}