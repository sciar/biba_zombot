using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class UnityEventListenerView : View
    {
        public Signal<bool> OnApplicationPausedSignal = new Signal<bool>();
        private bool _started;

        protected virtual void Start()
        {
            base.Start();
            _started = true;
        }

        void OnApplicationPause(bool pauseStatus) 
        {
            if (_started)
            {
                OnApplicationPausedSignal.Dispatch(pauseStatus);
            }
        }
    }
}

