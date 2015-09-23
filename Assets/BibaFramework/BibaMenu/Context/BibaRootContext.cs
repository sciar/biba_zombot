using System.Collections;
using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;
using BibaFramework.BibaTag;

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

        protected override void BindModels ()
        {
            injectionBinder.Bind<BibaSceneStack>().To<BibaSceneStack>().ToSingleton().CrossContext();
        }

        protected override void BindServices ()
        {
            injectionBinder.Bind<IBibaTagService>().To<VuforiaTagService>().ToSingleton().CrossContext();
        }

        protected override void BindViews ()
        {
            mediationBinder.Bind<BibaMenuStateMachineView>().To<BibaMenuStateMachineMediator>();
            mediationBinder.Bind<LoadingView>().To<LoadingMediator>();
        }

        protected override void BindCommands ()
        {
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
            injectionBinder.Bind<SetupMenuSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlayMenuEntryAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlayMenuExitedAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlayMenuLoadAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuEntryAnimationEndedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuExitAnimationEndedSignal>().ToSingleton().CrossContext();

            injectionBinder.Bind<TagScanningCompletedSignal>().ToSingleton().CrossContext();
        }
    }
}

