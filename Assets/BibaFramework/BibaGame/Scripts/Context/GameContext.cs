using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class GameContext : BaseBibaMenuContext 
    {
        public GameContext (MonoBehaviour view) : base(view)
        {
        }
        
        public GameContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<GameView>().To<GameMediator>();
        }
        
        protected override void BindCommands ()
        {   
            //TODO: bind it at round end
            commandBinder.Bind<EquipmentPlayedSignal>().To<EquipmentPlayedCommand>();
            commandBinder.Bind<TryToSetHighScoreSignal>().To<TryToSetHighScoreCommand>();
            commandBinder.Bind<EndSignal>().To<CheckForChartBoostCommand>();
        }
        
        protected override void BindSignals ()
        {
        }
    }
}