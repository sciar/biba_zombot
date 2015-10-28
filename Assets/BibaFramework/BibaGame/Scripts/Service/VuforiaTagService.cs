using System;
using Vuforia;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class VuforiaTagService : IBibaTagService
    {
        [Inject]
        public TagScannedSignal TagScannedSignal { get; set; }

        [Inject]
        public TagServiceInitFailedSignal TagServiceInitFailedSignal { get; set; }

        private BibaTrackableEventHandler[] _trackableEventHandlers;
        private BibaTrackableEventHandler[] TrackableEventHandlers {
            get {
                if(_trackableEventHandlers == null)
                {
                    _trackableEventHandlers = VuforiaBehaviour.Instance.GetComponentsInChildren<BibaTrackableEventHandler>();
                   
                }
                return _trackableEventHandlers;
            }
        }

        public void StartScan()
        {
            if (VuforiaBehaviour.Instance != null)
            {
                VuforiaBehaviour.Instance.RegisterVuforiaInitErrorCallback(TagInitFailed);
                Array.ForEach(TrackableEventHandlers, handler => handler.TrackingFoundSignal.AddListener(OnTagScanned));
            }
        }

        public void StopScan()
        {
            if (VuforiaBehaviour.Instance != null)
            {
                VuforiaBehaviour.Instance.UnregisterVuforiaInitErrorCallback(TagInitFailed);
                Array.ForEach(TrackableEventHandlers, handler => handler.TrackingFoundSignal.RemoveListener(OnTagScanned));
            }
        }

        void OnTagScanned(string fileName)
        {
            if(Enum.IsDefined(typeof(BibaTagType), fileName))
            {
                TagScannedSignal.Dispatch((BibaTagType)Enum.Parse(typeof(BibaTagType), fileName));
            }
        }

        void TagInitFailed(Vuforia.VuforiaUnity.InitError error)
        {
            Debug.LogWarning(error);
            TagServiceInitFailedSignal.Dispatch();
        }
    }
}