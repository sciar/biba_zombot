using System;
using Vuforia;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class VuforiaTagService : IBibaTagService
    {
        [Inject]
		public TagFoundSignal TagFoundSignal { get; set; }

		[Inject]
		public TagLostSignal TagLostSignal { get; set; }

        [Inject]
		public TagInitFailedSignal TagInitFailedSignal { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

        private BibaTrackableEventHandler[] TrackableEventHandlers {
            get {
                return VuforiaBehaviour.Instance.GetComponentsInChildren<BibaTrackableEventHandler>();
            }
        }

        public void StartScan()
        {
            if (VuforiaBehaviour.Instance != null)
            {
                VuforiaBehaviour.Instance.RegisterVuforiaInitErrorCallback(TagInitFailed);
				Array.ForEach(TrackableEventHandlers, handler => handler.TrackingFoundSignal.AddListener(OnTagFound));
				Array.ForEach(TrackableEventHandlers, handler => handler.TrackingLostSignal.AddListener(OnTagLost));
            }
        }

        public void StopScan()
        {
            if (VuforiaBehaviour.Instance != null)
            {
                VuforiaBehaviour.Instance.UnregisterVuforiaInitErrorCallback(TagInitFailed);
				Array.ForEach(TrackableEventHandlers, handler => handler.TrackingFoundSignal.RemoveListener(OnTagFound));
				Array.ForEach(TrackableEventHandlers, handler => handler.TrackingLostSignal.RemoveListener(OnTagLost));
			}
        }

		void OnTagFound(string fileName, Transform tagTransform)
        {
            if(Enum.IsDefined(typeof(BibaTagType), fileName))
            {
				BibaDeviceSession.TagTransform = tagTransform;
				BibaDeviceSession.TagCameraTransform = Camera.main.transform;
				TagFoundSignal.Dispatch((BibaTagType)Enum.Parse(typeof(BibaTagType), fileName));
            }
        }

		void OnTagLost(string fileName, Transform tagTransform)
		{
			if(Enum.IsDefined(typeof(BibaTagType), fileName))
			{
				BibaDeviceSession.TagTransform = null;
				BibaDeviceSession.TagCameraTransform = null;
				TagLostSignal.Dispatch((BibaTagType)Enum.Parse(typeof(BibaTagType), fileName));
			}
		}

        void TagInitFailed(Vuforia.VuforiaUnity.InitError error)
        {
            Debug.LogWarning(error);
			TagInitFailedSignal.Dispatch();
        }
    }
}