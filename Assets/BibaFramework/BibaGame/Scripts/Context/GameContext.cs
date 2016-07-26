using BibaFramework.BibaAnalytic;
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
			//For debugging from the GameScene in Unity
			#if UNITY_EDITOR
			var session = injectionBinder.GetInstance<BibaDeviceSession>();
			if(session.SelectedEquipments.Count == 0)
			{
				session.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.bridge));
				session.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.climber));
				session.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.overhang));
				session.TagEnabled = true;
			}
			#endif
        }
        
        protected override void BindServices ()
        {
	    }
        
        protected override void BindViews ()
        { 
            mediationBinder.Bind<GameView>().To<GameMediator>();
			mediationBinder.Bind<ScanningTagView>().To<ScanningTagMediator>();
			mediationBinder.Bind<VuforiaView> ().To<VuforiaMediator> ();
			mediationBinder.Bind<BibaTagEventHandlerView> ().To<BibaTagEventHandlerMediator> ();
        }
        
        protected override void BindCommands ()
        {   
            //TODO: bind it at round end
			commandBinder.Bind<StartSignal>().
				To<CheckForChartBoostCommand>().
				To<LogLastPlayedTimeCommand>();

			commandBinder.Bind<EquipmentPlayedSignal>().To<EquipmentPlayedCommand>();
			commandBinder.Bind<TryToSetHighScoreSignal>().To<TryToSetHighScoreCommand>();

			commandBinder.Bind<StartTagScanSignal>().To<StartTagScanCommand>();
			commandBinder.Bind<TagInitFailedSignal>().To<TagInitFailedCommand>();
			commandBinder.Bind<TagFoundSignal>().To<TagFoundCommand>();
			commandBinder.Bind<TagScanCompletedSignal>().
				To<CheckForFirstScanCompletedPointsEventCommand>().
				To<CheckForScanCompletedPointsEventCommand>();

			commandBinder.Bind<EndSignal>().
				To<RemoveVuforiaCommand>().
				To<CheckForAchievementsCommand>().
				To<CheckForFirstGameCompletedPointsEventCommand>().
				To<CheckForGameCompletedPointsEventCommand>().
				To<StartTrackingActivitiesCommand>();
        }
        
        protected override void BindSignals ()
        {
			injectionBinder.Bind<ToggleTagScanSignal>().To<ToggleTagScanSignal>().ToSingleton();
			injectionBinder.Bind<SetTagToScanAtViewSignal>().To<SetTagToScanAtViewSignal>().ToSingleton();
			injectionBinder.Bind<TagLostSignal>().ToSingleton();
        }
    }
}