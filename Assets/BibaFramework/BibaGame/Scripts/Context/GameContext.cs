using BibaFramework.BibaMenu;
using strange.extensions.context.api;
using UnityEngine;

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
			#if UNITY_EDITOR
			var gameModel = injectionBinder.GetInstance<BibaGameModel>();
			if(gameModel.SelectedEquipments.Count == 0)
			{
				gameModel.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.bridge));
				gameModel.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.climber));
				gameModel.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.overhang));
			}
			#endif
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
            commandBinder.Bind<StartSignal>().To<CheckForChartBoostCommand>();
            commandBinder.Bind<EquipmentPlayedSignal>().To<EquipmentPlayedCommand>();
            commandBinder.Bind<TryToSetHighScoreSignal>().To<TryToSetHighScoreCommand>();
            commandBinder.Bind<EndSignal>().To<CheckForAchievementsCommand>();
        }
        
        protected override void BindSignals ()
        {
        }
    }
}