using System.Collections;
using UnityEngine;
using BibaFramework.BibaMenu;
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

        protected override void BindModels ()
        {
        }

        protected override void BindServices ()
        {
        }

        protected override void BindViews ()
        {
            mediationBinder.Bind<BibaMenuStateMachineView>().To<BibaMenuStateMachineMediator>();
        }

        protected override void BindCommands ()
        {
            commandBinder.Bind<LoadGameSceneSignal>().To<LoadGameSceneCommand>();
            commandBinder.Bind<UnloadGameSceneSignal>().To<UnloadGameSceneCommand>();
        }

        protected override void BindSignals ()
        {
         //   injectionBinder.Bind<TriggerNextMenuStateSignal>().CrossContext();
            injectionBinder.Bind<SetupMenuSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlayMenuEntryAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PlayMenuExitedAnimationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuEntryAnimationEndedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MenuExitAnimationEndedSignal>().ToSingleton().CrossContext();
        }
    }
}

