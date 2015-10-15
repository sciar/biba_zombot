using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaMenu
{
    public class MenuStateMachineView : View
    {
		public Signal<BaseMenuState> MenuStateEnteredSignal  = new Signal<BaseMenuState>(); 

        public void EnteredMenuState(BaseMenuState menuState)
        {
            MenuStateEnteredSignal.Dispatch(menuState);
        }
    }
}