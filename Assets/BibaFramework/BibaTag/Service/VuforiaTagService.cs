using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;
using BibaFramework.BibaMenu;
using BibaFramework.Utility;

namespace BibaFramework.BibaTag
{
    public class VuforiaTagService : IBibaTagService
    {
        private const float SCAN_DURATION_SECONDS = 6f;

        [Inject]
        public TagScanningCompletedSignal TagScanningCompletedSignal { get; set; }

        private HashSet<BibaTag> _lastScannedTags;
        public HashSet<BibaTag> LastScannedTags {
            get {
                if(_lastScannedTags == null)
                {
                    _lastScannedTags = new HashSet<BibaTag>();
                    VuforiaBehaviour.Instance.gameObject.GetComponentsInChildren<BibaTrackableEventHandler>().ToList().ForEach(handler => {

                        handler.TrackingFoundSignal.RemoveListener(TrackingFound);
                        handler.TrackingFoundSignal.AddListener(TrackingFound);
                    });
                }
                return _lastScannedTags;
            }
            set {
                _lastScannedTags = value;
            }
        }

        public void StartScanWithCompleteHandler()
        {
            new Task(StartScanning(), true);
        }

        IEnumerator StartScanning()
        {
            VuforiaBehaviour.Instance.enabled = true;
            LastScannedTags.Clear();

            yield return new WaitForSeconds(SCAN_DURATION_SECONDS);

            TagScanningCompletedSignal.Dispatch();
            VuforiaBehaviour.Instance.enabled = false;
        }

       void TrackingFound(string itemName)
        {
            if(Enum.IsDefined(typeof(BibaTag), itemName))
            {
                LastScannedTags.Add((BibaTag)Enum.Parse(typeof(BibaTag), itemName));
            }
        }
    }
}