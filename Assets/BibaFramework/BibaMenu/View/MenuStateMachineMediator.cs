using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class MenuStateMachineMediator : Mediator
    {
        [Inject]
        public MenuStateMachineView View { get; set; }

        [Inject]
        public ProcessNextMenuStateSignal ProcessNextMenuStateSignal { get; set; }

        public override void OnRegister ()
        {
            View.MenuStateEnteredSignal.AddListener(OnMenuStateEntered);
        }

        public override void OnRemove ()
        {
            View.MenuStateEnteredSignal.RemoveListener(OnMenuStateEntered);
        }

        void OnMenuStateEntered(BaseMenuState menuState)
        {
            ProcessNextMenuStateSignal.Dispatch(menuState);
        }
    }
}