using strange.extensions.mediation.impl;
using strange.extensions.context.impl;
using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    public abstract class SceneMenuStateMediator : Mediator, IAudioView
    {
        [Inject]
        public SetupSceneMenuStateSignal SetupMenuSignal { get; set; }

        [Inject]
        public MenuStateEntryAnimationEndedSignal SceneMenuStateEntryAnimationEndedSignal { get; set; }

        [Inject]
        public MenuStateExitAnimationEndedSignal SceneMenuStateExitAnimationEndedSignal { get; set; }

        [Inject]
        public AudioServices AudioServices { get; set; }

        public abstract SceneMenuStateView View { get; }

        public override void OnRegister ()
        {
            View.AudioServices = AudioServices;
            SetupMenuSignal.AddListener(SetupMenu);
            RegisterSceneDependentSignals();
        }

        public override void OnRemove ()
        {
            SetupMenuSignal.RemoveListener(SetupMenu);
            UnRegisterSceneDependentSignals();
        }

        public abstract void RegisterSceneDependentSignals();
        public abstract void UnRegisterSceneDependentSignals();
        public abstract void SetupMenu(BaseMenuState menuState);

        public void AnimateMenuEntry()
        {
            //TODO: hack fix sometimes mediator is still there after the gameobject has been destroyed by Unity
            if (View != null)
            {
                View.StartEntryAnimation(() => SceneMenuStateEntryAnimationEndedSignal.Dispatch());
            }
        }

        public void AnimateMenuExit()
        {
            //TODO: hack fix sometimes mediator is still there after the gameobject has been destroyed by Unity
            if (View != null)
            {
                View.StartExitAnimation(() => SceneMenuStateExitAnimationEndedSignal.Dispatch());
            }
        }
    }
}

