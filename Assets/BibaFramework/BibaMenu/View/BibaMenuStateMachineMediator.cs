using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class BibaMenuStateMachineMediator : Mediator
    {
        [Inject]
        public BibaMenuStateMachineView View { get; set; }

        [Inject]
        public LoadGameSceneSignal LoadGameSceneSignal { get; set; }

        [Inject]
        public UnloadGameSceneSignal UnloadGameSceneSignal { get; set; }

        public override void OnRegister ()
        {
            View.MenuStateEnteredSignal.AddListener(OnMenuStateEntered);
            View.MenuStateExitedSignal.AddListener(OnMenuStateExited);
        }

        public override void OnRemove ()
        {
            View.MenuStateEnteredSignal.RemoveListener(OnMenuStateEntered);
            View.MenuStateExitedSignal.RemoveListener(OnMenuStateExited);
        }

        void OnMenuStateEntered(BibaMenuState menuState)
        {
            Debug.Log("MenuState: " + menuState.GameScene.ToString() + " entered.");
            LoadGameSceneSignal.Dispatch(menuState);
        }

        void OnMenuStateExited(BibaMenuState menuState)
        {
            Debug.Log("MenuState: " + menuState.GameScene.ToString() + " exited.");
            UnloadGameSceneSignal.Dispatch(menuState);
        }
    }
}

