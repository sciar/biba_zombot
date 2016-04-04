using System.Collections;
using UnityEngine;
using BibaFramework.BibaAnalytic;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using BibaFramework.BibaNetwork;
using strange.extensions.context.api;

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

            //Create a bogus binding for the BibaGameModel because we are going to rebind it in LoadGameDataCommand
            injectionBinder.Bind<BibaGameModel>().To<BibaGameModel>();
            injectionBinder.Bind<BibaSessionModel>().To<BibaSessionModel>().ToSingleton().CrossContext();
        }

        protected override void BindServices ()
		{
			injectionBinder.Bind<IAnalyticService>().To<FlurryAnalyticService>().ToSingleton().CrossContext();
			injectionBinder.Bind<IDataService>().To<JSONDataService>().ToSingleton().CrossContext();
            injectionBinder.Bind<ICDNService>().To<BibaCDNService>().ToSingleton().CrossContext();
            injectionBinder.Bind<LocalizationService>().To<LocalizationService>().ToSingleton().CrossContext();
            injectionBinder.Bind<AchievementService>().To<AchievementService>().ToSingleton().CrossContext();
            injectionBinder.Bind<SpecialSceneService>().To<SpecialSceneService>().ToSingleton().CrossContext();
        }

        protected override void BindViews ()
        {
            mediationBinder.Bind<MenuStateMachineView>().To<MenuStateMachineMediator>();
            mediationBinder.Bind<UnityEventListenerView>().To<UnityEventListenerMediator>();
        }

        protected override void BindCommands ()
        {
            //BibaGame

			//Start setup - order is important
            commandBinder.Bind<StartSignal>().
					To<SetupServicesCommand>().
                    To<LoadGameModelCommand>().
                    To<SetupGameModelCommand>().
                    To<SetupGameConfigCommand>().
                    To<SetupEditorDebugSceneCommand>().
                    InSequence();
     
            commandBinder.Bind<ApplicationPausedSignal>().
                To<LogLastPlayedTimeCommand>().InSequence();
           
            commandBinder.Bind<ApplicationUnPausedSignal>().
                    To<LogLocationInfoCommand>().
                    To<CheckForDownloadContentCommand>().
                    To<CheckForInactiveResetCommand>().InSequence();

			commandBinder.Bind<SetLanguageOverwriteSignal> ().To<SetLanguageOverwriteCommand> ();

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
            //BibaMenu
            injectionBinder.Bind<GameModelUpdatedSignal>().To<GameModelUpdatedSignal>().ToSingleton().CrossContext();

            injectionBinder.Bind<SetupSceneMenuStateSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuStateEntryAnimationEndedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuStateExitAnimationEndedSignal>().ToSingleton().CrossContext();
            
            injectionBinder.Bind<ToggleObjectMenuStateSignal>().ToSingleton().CrossContext();

            //BibaTag
            injectionBinder.Bind<TagScannedSignal>().ToSingleton().CrossContext();
        }
    }
}

