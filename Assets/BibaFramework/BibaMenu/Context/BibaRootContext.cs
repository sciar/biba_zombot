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
            injectionBinder.Bind<IBibaTagService>().To<ARToolKitTagService>().ToSingleton().CrossContext();
        }

        protected override void BindViews ()
        {
            mediationBinder.Bind<BibaMenuStateMachineView>().To<BibaMenuStateMachineMediator>();
            mediationBinder.Bind<ARToolKitView>().To<ARToolKitMediator>();
            mediationBinder.Bind<LoadingView>().To<LoadingMediator>();
            mediationBinder.Bind<UnityEventListenerView>().To<UnityEventListenerMediator>();
        }

        protected override void BindCommands ()
        {
            //BibaGame
            commandBinder.Bind<StartSignal>().
                To<LoadGameDataCommand>().
                    To<SetupGameStateMachineCommand>().
                    To<CheckPrivacyStatementAcceptedCommand>().
                    To<SetupAnalyticCommand>().InSequence();

            commandBinder.Bind<ApplicationPausedSignal>().
                To<LogLastPlayedTimeCommand>().InSequence();
           
            commandBinder.Bind<ApplicationUnPausedSignal>().
                To<CheckForInactiveResetCommand>().InSequence();

            //BibaMenu
            commandBinder.Bind<ProcessNextMenuStateSignal>().To<ProcessNextMenuStateCommand>();

            commandBinder.Bind<LoadFullSceneSignal>()
                    .To<DisableAllInputCommand>()
                    .To<AnimateSceneExitCommand>()
                    .To<ClearAllViewsCommand>()
                    .To<PushNewViewCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<PushPopupSceneSignal>()
                    .To<DisableAllInputCommand>()
                    .To<PushNewViewCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<PopPopupSceneSignal>()
                    .To<DisableAllInputCommand>()
                    .To<AnimateSceneExitCommand>()
                    .To<PopLastViewCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<ReplacePopupSceneSignal>()
                    .To<DisableAllInputCommand>()    
                    .To<AnimateSceneExitCommand>()
                    .To<PopLastViewCommand>()
                    .To<PushNewViewCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();
        }

        protected override void BindSignals ()
        {
            //BibaMenu
            injectionBinder.Bind<SetupMenuSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlayMenuEntryAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlayMenuExitedAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlayMenuLoadAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuEntryAnimationEndedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuExitAnimationEndedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<SetMenuStateTriggerSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<SetMenuStateConditionSignal>().ToSingleton().CrossContext();

            //BibaTag
            injectionBinder.Bind<ToggleScanSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<TagScannedSignal>().ToSingleton().CrossContext();
        }
    }
}

