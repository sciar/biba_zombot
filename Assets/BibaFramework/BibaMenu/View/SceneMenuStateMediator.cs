using strange.extensions.mediation.impl;
using strange.extensions.context.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public abstract class SceneMenuStateMediator : Mediator 
    {
        [Inject]
        public SetupSceneMenuStateSignal SetupMenuSignal { get; set; }

        [Inject]
        public PlayMenuStateEntryAnimationSignal PlaySceneMenuStateEntryAnimationSignal { get; set; }
       
        [Inject]
        public PlayMenuStateExitAnimationSignal PlaySceneMenuStateExitAnimationSignal { get; set; }

        [Inject]
        public MenuStateEntryAnimationEndedSignal SceneMenuStateEntryAnimationEndedSignal { get; set; }

        [Inject]
        public MenuStateExitAnimationEndedSignal SceneMenuStateExitAnimationEndedSignal { get; set; }

        public abstract SceneMenuStateView View { get; }

        public override void OnRegister ()
        {
            PlaySceneMenuStateEntryAnimationSignal.AddListener(AnimateMenuEntry);
            PlaySceneMenuStateExitAnimationSignal.AddListener(AnimateMenuExit);
            SetupMenuSignal.AddListener(SetupMenu);

            RegisterSceneDependentSignals();
        }

        public override void OnRemove ()
        {
            PlaySceneMenuStateEntryAnimationSignal.RemoveListener(AnimateMenuEntry);
            PlaySceneMenuStateExitAnimationSignal.RemoveListener(AnimateMenuExit);
            SetupMenuSignal.RemoveListener(SetupMenu);

            RemoveSceneDependentSignals();
        }

        public abstract void RegisterSceneDependentSignals();
        public abstract void RemoveSceneDependentSignals();
        public abstract void SetupMenu(BaseMenuState menuState);

        void AnimateMenuEntry()
        {
            //TODO: hack fix sometimes mediator is still there after the gameobject has been destroyed by Unity
            if (View != null)
            {
                View.StartEntryAnimation(() => SceneMenuStateEntryAnimationEndedSignal.Dispatch());
            }
        }

        void AnimateMenuExit()
        {
            //TODO: hack fix sometimes mediator is still there after the gameobject has been destroyed by Unity
            if (View != null)
            {
                View.StartExitAnimation(() => SceneMenuStateExitAnimationEndedSignal.Dispatch());
            }
        }
    }
}

