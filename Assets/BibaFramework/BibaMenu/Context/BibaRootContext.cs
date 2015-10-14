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
            mediationBinder.Bind<BibaMenuStateMachineView>().To<BibaMenuStateMachineMediator>();
            mediationBinder.Bind<UnityEventListenerView>().To<UnityEventListenerMediator>();
        }

        protected override void BindCommands ()
        {
            //BibaGame
            commandBinder.Bind<StartSignal>().
                To<LoadGameDataCommand>().
                    To<SetupAnalyticCommand>().
                    To<SetupGameStateMachineCommand>().
                    To<CheckPrivacyStatementAcceptedCommand>().
                    To<SetupEditorGameSceneCommand>().
                    InSequence();
     
            commandBinder.Bind<ApplicationPausedSignal>().
                To<LogLastPlayedTimeCommand>().InSequence();
           
            commandBinder.Bind<ApplicationUnPausedSignal>().
                To<CheckForInactiveResetCommand>().InSequence();

            //BibaMenu
            commandBinder.Bind<ProcessNextMenuStateSignal>().To<ProcessNextMenuStateCommand>();

            //BibaMenu - GameObject
            //TODO:limit input and wait for animation
            commandBinder.Bind<EnableObjectBasedMenuStateSignal>()
                .To<EnableObjectBasedMenuStateCommand>();
                    

            //BibaMenu - GameScene
            commandBinder.Bind<LoadSceneBasedMenuStateSignal>()
                    .To<DisableAllInputCommand>()
                    .To<AnimateSceneBasedMenuStateExitCommand>()
                    .To<ClearAllViewsCommand>()
                    .To<PushNewViewCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneBasedMenuStateEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<PushSceneBasedMenuStateSignal>()
                    .To<DisableAllInputCommand>()
                    .To<PushNewViewCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneBasedMenuStateEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<PopSceneBasedMenuStateSignal>()
                    .To<DisableAllInputCommand>()
                    .To<AnimateSceneBasedMenuStateExitCommand>()
                    .To<PopLastViewCommand>()
                    .To<EnableTopInputCommand>().InSequence();

            commandBinder.Bind<ReplaceSceneBasedMenuStateSignal>()
                    .To<DisableAllInputCommand>()    
                    .To<AnimateSceneBasedMenuStateExitCommand>()
                    .To<PopLastViewCommand>()
                    .To<PushNewViewCommand>()
                    .To<DisableTopInputCommand>()
                    .To<AnimateSceneBasedMenuStateEntryCommand>()
                    .To<EnableTopInputCommand>().InSequence();
        }

        protected override void BindSignals ()
        {
            //BibaMenu
            injectionBinder.Bind<SetupMenuSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlaySceneBasedMenuStateEntryAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlaySceneBasedMenuStateExitAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<SceneBasedMenuStateEntryAnimationEndedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<SceneBasedMenuStateExitAnimationEndedSignal>().ToSingleton().CrossContext();

            //BibaTag
            injectionBinder.Bind<TagScannedSignal>().ToSingleton().CrossContext();
        }
    }
}

