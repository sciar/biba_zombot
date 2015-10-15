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
                    To<SetupMonoBehaviourServices>().
                    To<CheckPrivacyStatementAcceptedCommand>().
                    To<SetupEditorDebugSceneCommand>().
                    InSequence();
     
            commandBinder.Bind<ApplicationPausedSignal>().
                To<LogLastPlayedTimeCommand>().InSequence();
           
            commandBinder.Bind<ApplicationUnPausedSignal>().
                To<CheckForInactiveResetCommand>().InSequence();

            //BibaMenu
            commandBinder.Bind<ProcessNextMenuStateSignal>().To<ProcessNextMenuStateCommand>();

			//BibaMenu - GameScene
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
        }

        protected override void BindSignals ()
        {
            //BibaMenu
            injectionBinder.Bind<SetupSceneMenuStateSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlayMenuStateEntryAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlayMenuStateExitAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuStateEntryAnimationEndedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuStateExitAnimationEndedSignal>().ToSingleton().CrossContext();
			
			injectionBinder.Bind<ToggleObjectMenuStateSignal>().ToSingleton().CrossContext();

            //BibaTag
            injectionBinder.Bind<TagScannedSignal>().ToSingleton().CrossContext();
        }
    }
}

