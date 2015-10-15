using System.Collections;
using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;
using BibaFramework.BibaTag;
using BibaFramework.BibaGame;
using BibaFramework.BibaData;
using BibaFramework.BibaAnalytic;

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
        }

        protected override void BindServices ()
        {
            injectionBinder.Bind<IDataService>().To<JSONDataService>().ToSingleton().CrossContext();
        }

        protected override void BindViews ()
        {
            mediationBinder.Bind<MenuStateMachineView>().To<MenuStateMachineMediator>();
            mediationBinder.Bind<UnityEventListenerView>().To<UnityEventListenerMediator>();
        }

        protected override void BindCommands ()
        {
            //BibaGame
            commandBinder.Bind<StartSignal>().
                To<LoadGameDataCommand>().
                    To<SetupAnalyticCommand>().
                    To<SetupStateMachineCommand>().
                    To<CheckPrivacyStatementAcceptedCommand>().
                    To<SetupEditorDebugSceneCommand>().
                    InSequence();
     
            commandBinder.Bind<ApplicationPausedSignal>().
                To<LogLastPlayedTimeCommand>().InSequence();
           
            commandBinder.Bind<ApplicationUnPausedSignal>().
                To<CheckForInactiveResetCommand>().InSequence();

            //BibaMenu
            commandBinder.Bind<ProcessNextMenuStateSignal>().To<ProcessNextMenuStateCommand>();

            //BibaMenu - GameObject
            //TODO:limit input and wait for animation
            commandBinder.Bind<PushObjectMenuStateSignal>()
				.To<DisableAllInputCommand>()
                .To<EnableObjectMenuStateCommand>()
					.To<EnableTopInputCommand>();
			
			commandBinder.Bind<ReplaceObjectMenuStateSignal>()
				.To<DisableAllInputCommand>()
					.To<RemoveLastMenuStateCommand>()
					.To<EnableObjectMenuStateCommand>()
					.To<EnableTopInputCommand>();

			//BibaMenu - GameScene
            commandBinder.Bind<LoadSceneMenuStateSignal>()
                    .To<DisableAllInputCommand>()
                    .To<AnimateSceneMenuStateExitCommand>()
                    .To<RemoveAllMenuStatesCommand>()
                    .To<LoadNewSceneMenuStateCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneMenuStateEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<PushSceneMenuStateSignal>()
                    .To<DisableAllInputCommand>()
                    .To<LoadNewSceneMenuStateCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneMenuStateEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<PopSceneMenuStateSignal>()
                    .To<DisableAllInputCommand>()
                    .To<AnimateSceneMenuStateExitCommand>()
                    .To<RemoveLastMenuStateCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<ReplaceSceneMenuStateSignal>()
                    .To<DisableAllInputCommand>()    
                    .To<AnimateSceneMenuStateExitCommand>()
                    .To<RemoveLastMenuStateCommand>()
                    .To<LoadNewSceneMenuStateCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneMenuStateEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();
        }

        protected override void BindSignals ()
        {
            //BibaMenu
            injectionBinder.Bind<SetupSceneMenuStateSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlaySceneMenuStateEntryAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlaySceneMenuStateExitAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<SceneMenuStateEntryAnimationEndedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<SceneMenuStateExitAnimationEndedSignal>().ToSingleton().CrossContext();
			
			injectionBinder.Bind<ToggleObjectMenuStateSignal>().ToSingleton().CrossContext();

            //BibaTag
            injectionBinder.Bind<TagScannedSignal>().ToSingleton().CrossContext();
        }
    }
}

