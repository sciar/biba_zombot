using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaMenu
{
    public class BibaMenuStateMachineView : View
    {
        public Signal<BibaMenuState> MenuStateEnteredSignal  = new Signal<BibaMenuState>(); 

        public void EnteredMenuState(BibaMenuState menuState)
        {
            MenuStateEnteredSignal.Dispatch(menuState);
        }
    }
}