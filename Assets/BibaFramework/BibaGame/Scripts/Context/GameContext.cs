using BibaFramework.BibaMenu;
using strange.extensions.context.api;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

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
			var sessionModel = injectionBinder.GetInstance<BibaSessionModel>();
			if(sessionModel.SelectedEquipments.Count == 0)
			{
				sessionModel.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.bridge));
				sessionModel.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.climber));
				sessionModel.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.overhang));
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