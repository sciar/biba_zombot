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

        public override void OnRegister ()
        {
            View.MenuStateEnteredSignal.AddListener(OnMenuStateEntered);
        }

        public override void OnRemove ()
        {
            View.MenuStateEnteredSignal.RemoveListener(OnMenuStateEntered);
        }

        void OnMenuStateEntered(BibaMenuState menuState)
        {
            LoadGameSceneSignal.Dispatch(menuState);
        }
    }
}