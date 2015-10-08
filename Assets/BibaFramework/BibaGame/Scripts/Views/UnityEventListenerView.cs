using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace BibaFramework.BibaGame
{
    public class UnityEventListenerView : View
    {
        public Signal<bool> OnApplicationPausedSignal = new Signal<bool>();

        void OnApplicationPause(bool pauseStatus) {
            OnApplicationPausedSignal.Dispatch(pauseStatus);
        }
    }
}

