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
        public PlayBibaBGMSignal PlayBibaBGMSignal { get; set; }
       
        [Inject]
        public PlayBibaSFXSignal PlayBibaSFXSignal { get; set; }

        [Inject]
        public AudioServices AudioServices { get; set; }

        public abstract SceneMenuStateView View { get; }

        protected BaseMenuState MenuState { get; private set; }

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

        public void SetupMenu(BaseMenuState menuState)
        {
            MenuState = menuState;
            SetupSceneDependentMenu();
        }

        public abstract void SetupSceneDependentMenu();

        public void AnimateMenuEntry()
        {
            //TODO: hack fix sometimes mediator is still there after the gameobject has been destroyed by Unity
            if (View != null)
            {
                View.StartEntryAnimation(() => SceneMenuStateEntryAnimationEndedSignal.Dispatch());

                if(MenuState.EnterBGM != BibaBGM.None)
                {
                    PlayBibaBGMSignal.Dispatch(MenuState.EnterBGM);
                }

                if(MenuState.EnterSFX != BibaSFX.None)
                {
                    PlayBibaSFXSignal.Dispatch(MenuState.EnterSFX);
                }
            }
        }

        public void AnimateMenuExit()
        {
            //TODO: hack fix sometimes mediator is still there after the gameobject has been destroyed by Unity
            if (View != null)
            {
                View.StartExitAnimation(() => SceneMenuStateExitAnimationEndedSignal.Dispatch());

                if(MenuState.ExitBGM != BibaBGM.None)
                {
                    PlayBibaBGMSignal.Dispatch(MenuState.ExitBGM);
                }
                
                if(MenuState.ExitSFX != BibaSFX.None)
                {
                    PlayBibaSFXSignal.Dispatch(MenuState.ExitSFX);
                }
            }
        }
    }
}