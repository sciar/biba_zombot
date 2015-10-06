using strange.extensions.mediation.impl;
using strange.extensions.context.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public abstract class BaseBibaMediator : Mediator 
    {
        [Inject]
        public PlayMenuEntryAnimationSignal PlayMenuEntryAnimationSignal { get; set; }
       
        [Inject]
        public PlayMenuExitedAnimationSignal PlayMenuExitedAnimationSignal { get; set; }

        [Inject]
        public SetupMenuSignal SetupMenuSignal { get; set; }

        [Inject]
        public MenuEntryAnimationEndedSignal MenuEntryAnimationEndedSignal { get; set; }

        [Inject]
        public MenuExitAnimationEndedSignal MenuExitAnimationEndedSignal { get; set; }

        public abstract BaseBibaView View { get; }

        public override void OnRegister ()
        {
            PlayMenuEntryAnimationSignal.AddListener(AnimateMenuEntry);
            PlayMenuExitedAnimationSignal.AddListener(AnimateMenuExit);
            SetupMenuSignal.AddListener(SetupMenu);

            RegisterSceneDependentSignals();
        }

        public override void OnRemove ()
        {
            PlayMenuEntryAnimationSignal.RemoveListener(AnimateMenuEntry);
            PlayMenuExitedAnimationSignal.RemoveListener(AnimateMenuExit);
            SetupMenuSignal.RemoveListener(SetupMenu);

            RemoveSceneDependentSignals();
        }

        public abstract void RegisterSceneDependentSignals();
        public abstract void RemoveSceneDependentSignals();
        public abstract void SetupMenu(BibaMenuState menuState);

        void AnimateMenuEntry()
        {
            View.StartEntryAnimation(() => MenuEntryAnimationEndedSignal.Dispatch());
        }

        void AnimateMenuExit()
        {
            View.StartExitAnimation(() => MenuExitAnimationEndedSignal.Dispatch());
        }
    }
}

