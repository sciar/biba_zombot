using System;
using Vuforia;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaTag
{
    public class VuforiaTagService : IBibaTagService
    {
        [Inject]
        public TagScannedSignal TagScannedSignal { get; set; }

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
            Array.ForEach(TrackableEventHandlers, handler => handler.TrackingFoundSignal.AddListener(OnTagScanned));
        }

        public void StopScan()
        {
            Array.ForEach(TrackableEventHandlers, handler => handler.TrackingFoundSignal.RemoveListener(OnTagScanned));
        }

        void OnTagScanned(string fileName)
        {
            if(Enum.IsDefined(typeof(BibaTagType), fileName))
            {
                TagScannedSignal.Dispatch((BibaTagType)Enum.Parse(typeof(BibaTagType), fileName));
            }
        }
    }
}