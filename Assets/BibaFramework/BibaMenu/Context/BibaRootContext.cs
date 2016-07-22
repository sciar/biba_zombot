using System.Collections;
using BibaFramework.BibaAnalytic;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using BibaFramework.BibaNetwork;
using strange.extensions.context.api;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class BibaRootContext : BaseBibaContext 
    {
        public BibaRootContext (MonoBehaviour view) : base(view)
        {
        }
        
        public BibaRootContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void BindBaseComponents ()
        {
            base.BindBaseComponents();
            injectionBinder.Bind<GameObject>().To((GameObject)contextView).ToName(BibaMenuConstants.BIBA_ROOT_CONTEXT_VIEW);
        }

        protected override void BindModels ()
        {
            injectionBinder.Bind<BibaSceneStack>().To<BibaSceneStack>().ToSingleton().CrossContext();
			injectionBinder.Bind<BibaDeviceSession>().To<BibaDeviceSession>().ToSingleton().CrossContext();

			injectionBinder.Bind<BibaAccount>().To<BibaAccount>();
			injectionBinder.Bind<BibaProfile>().To<BibaProfile>();
			injectionBinder.Bind<BibaDevice>().To<BibaDevice>();
        }

        protected override void BindServices ()
		{
			injectionBinder.Bind<IAnalyticService>().To<FlurryAnalyticService>().ToSingleton().CrossContext();
			injectionBinder.Bind<IDataService>().To<JSONDataService>().ToSingleton().CrossContext();
            injectionBinder.Bind<ICDNService>().To<BibaCDNService>().ToSingleton().CrossContext();
            injectionBinder.Bind<LocalizationService>().To<LocalizationService>().ToSingleton().CrossContext();
            injectionBinder.Bind<AchievementService>().To<AchievementService>().ToSingleton().CrossContext();
            injectionBinder.Bind<SpecialSceneService>().To<SpecialSceneService>().ToSingleton().CrossContext();
			injectionBinder.Bind<PointEventService>().To<PointEventService>().ToSingleton().CrossContext();
        }

        protected override void BindViews ()
        {
            mediationBinder.Bind<MenuStateMachineView>().To<MenuStateMachineMediator>();
            mediationBinder.Bind<UnityEventListenerView>().To<UnityEventListenerMediator>();
			mediationBinder.Bind<PointsPopupView> ().To<PointsPopupMediator> ();
        }

        protected override void BindCommands ()
        {
            //BibaGame

			//Start setup - order is important
            commandBinder.Bind<StartSignal>().
					To<SetServicesCommand>().
                    To<SetModelsCommand>().
					To<SetDefaultProfileCommand>().
                    To<SetDeviceModelCommand>().
					To<StartNewSessionCommand>().
					To<UpdateFromCDNCommand>().
					To<CheckForFirstStartPointsEventCommand>().
			#if UNITY_EDITOR
					To<SetupEditorDebugSceneCommand>().
			#endif
                    InSequence();

			commandBinder.Bind<ApplicationPausedSignal>().
				To<LogLocationInfoCommand>().
				To<EndTrackingActivitiesCommand>();

            commandBinder.Bind<ApplicationUnPausedSignal>().
					To<UpdateFromCDNCommand>().
                    To<LogLocationInfoCommand>().
					To<StartTrackingActivitiesCommand>().
                    //TODO: reenable when location based theme is up
					//To<CheckForDownloadContentCommand>().
					To<CheckForSessionEndCommand>();

			commandBinder.Bind<SetProfileSignal> ().To<SetProfileCommand> ();

            //BibaMenu
            commandBinder.Bind<ProcessNextMenuStateSignal>().To<ProcessNextMenuStateCommand>();

            commandBinder.Bind<SwitchSceneMenuStateSignal>()
                    .To<DisableAllInputCommand>()
                    .To<AnimateSceneMenuStateExitCommand>()
                    .To<RemoveAllMenuStatesCommand>()
                    .To<LoadNewMenuStateCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneMenuStateEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<PushMenuStateSignal>()
                .To<DisableAllInputCommand>()
                    .To<LoadNewMenuStateCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneMenuStateEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<PopMenuStateSignal>()
                .To<DisableAllInputCommand>()
                    .To<AnimateSceneMenuStateExitCommand>()
                    .To<RemoveLastMenuStateCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<ReplaceMenuStateSignal>()
                .To<DisableAllInputCommand>()    
                    .To<AnimateSceneMenuStateExitCommand>()
                    .To<RemoveLastMenuStateCommand>()
                    .To<LoadNewMenuStateCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneMenuStateEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<RemoveLastMenuStateSignal>().To<RemoveLastMenuStateCommand>();
        }

        protected override void BindSignals ()
        {
			//BibaGame
			injectionBinder.Bind<SessionUpdatedSignal>().ToSingleton().CrossContext();
          	injectionBinder.Bind<SystemUpdatedSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<PointsGainedSignal> ().ToSingleton ().CrossContext ();

			//BibaMenu
            injectionBinder.Bind<SetupSceneMenuStateSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuStateEntryAnimationEndedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuStateExitAnimationEndedSignal>().ToSingleton().CrossContext();
            
            injectionBinder.Bind<ToggleObjectMenuStateSignal>().ToSingleton().CrossContext();
        }
    }
}