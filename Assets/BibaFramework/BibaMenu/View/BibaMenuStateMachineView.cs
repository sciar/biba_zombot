using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class BibaMenuStateMachineView : View
    {
        public Signal<BibaMenuState> MenuStateEnteredSignal  = new Signal<BibaMenuState>(); 
        public Signal<BibaMenuState> MenuStateExitedSignal = new Signal<BibaMenuState>(); 

        public void EnteredMenuState(BibaMenuState menuState)
        {
            MenuStateEnteredSignal.Dispatch(menuState);
        }

        public void ExitedMenuState(BibaMenuState menuState)
        {
            MenuStateExitedSignal.Dispatch(menuState);
        }
    }
}