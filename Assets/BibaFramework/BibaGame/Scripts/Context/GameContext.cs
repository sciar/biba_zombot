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
			injectionBinder.Bind<IBibaTagService> ().To<VuforiaTagService> ();
        }
        
        protected override void BindViews ()
        { 
            mediationBinder.Bind<GameView>().To<GameMediator>();
			mediationBinder.Bind<ScanningTagView>().To<ScanningTagMediator>();
        }
        
        protected override void BindCommands ()
        {   
            //TODO: bind it at round end
			commandBinder.Bind<StartSignal>().
				To<CheckForChartBoostCommand>().
				To<LogLastPlayedTimeCommand>();

			commandBinder.Bind<EquipmentPlayedSignal>().To<EquipmentPlayedCommand>();
			commandBinder.Bind<TryToSetHighScoreSignal>().To<TryToSetHighScoreCommand>();

			commandBinder.Bind<SetTagToScanSignal>().To<SetTagToScanCommand>();
			commandBinder.Bind<LogCameraReminderTimeSignal>().To<LogCameraReminderTimeCommand>();
			commandBinder.Bind<TagScanCompletedSignal>().
				To<TagScanCompletedCommand>().
				To<RemoveVuforiaCommand>().
				To<CheckForFirstScanCompletedPointsEventCommand>().
				To<CheckForScanCompletedPointsEventCommand>();

			commandBinder.Bind<EndSignal>().
				To<CheckForAchievementsCommand>().
				To<CheckForFirstGameCompletedPointsEventCommand>().
				To<CheckForGameCompletedPointsEventCommand>().
				To<StartTrackingActivitiesCommand>();
        }
        
        protected override void BindSignals ()
        {
			injectionBinder.Bind<SetTagToScanAtViewSignal>().To<SetTagToScanAtViewSignal>().ToSingleton();
			injectionBinder.Bind<TagInitFailedSignal>().To<TagInitFailedSignal>().ToSingleton();
			injectionBinder.Bind<TagFoundSignal>().ToSingleton();
			injectionBinder.Bind<TagLostSignal>().ToSingleton();
        }
    }
}