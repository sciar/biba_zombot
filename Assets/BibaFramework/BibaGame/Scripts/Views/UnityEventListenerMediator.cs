
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace BibaFramework.BibaGame
{
    public class UnityEventListenerMediator : Mediator
    {
        [Inject]
        public UnityEventListenerView UnityEventListenerView { get; set; }

        [Inject]
        public ApplicationPausedSignal ApplicationPausedSignal { get; set; }

        [Inject]
        public ApplicationUnPausedSignal ApplicationUnPausedSignal { get; set; }

        public override void OnRegister ()
        {
            UnityEventListenerView.OnApplicationPausedSignal.AddListener(SendApplicationPausedSignal);
        }

        public override void OnRemove ()
        {
            UnityEventListenerView.OnApplicationPausedSignal.RemoveListener(SendApplicationPausedSignal);
        }

        void SendApplicationPausedSignal(bool status)
        {
            if (status)
            {
                ApplicationPausedSignal.Dispatch();
            }
            else 
            { 
                ApplicationUnPausedSignal.Dispatch();
            }
        }
    }
}