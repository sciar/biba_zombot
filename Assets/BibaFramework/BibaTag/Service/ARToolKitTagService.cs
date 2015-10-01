using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BibaFramework.Utility;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaTag
{
    public class ARToolKitTagService : IBibaTagService
    {
        [Inject]
        public ToggleScanSignal ToggleScanSignal { get; set; }

        [Inject]
        public TagScannedSignal TagScannedSignal { get; set; }

        private HashSet<BibaTagType> _lastScannedTags;
        public HashSet<BibaTagType> LastScannedTags {
            get {
                if(_lastScannedTags == null)
                {
                    TagScannedSignal.AddListener(OnTagScanned);
                    _lastScannedTags = new HashSet<BibaTagType>();
  
                }
                return _lastScannedTags;
            }
            set {
                _lastScannedTags = value;
            }
        }

        public void StartScan()
        {
            LastScannedTags.Clear();
            ToggleScanSignal.Dispatch(true);
        }

        public void StopScan()
        {
            ToggleScanSignal.Dispatch(false);
        }

        public void StartScanWithCompleteHandler (Func<int, bool> isCompleted, Action onCompleted)
        {
            new Task(StartScanning(isCompleted, onCompleted), true);
        }

        IEnumerator StartScanning(Func<int, bool> isCompleted, Action onCompleted)
        {
            StartScan();

            while (!isCompleted(LastScannedTags.Count))
            {
                yield return null;
            }

            StopScan();
            onCompleted();
        }

        void OnTagScanned(string fileName)
        {
            if(Enum.IsDefined(typeof(BibaTagType), fileName))
            {
                LastScannedTags.Add((BibaTagType)Enum.Parse(typeof(BibaTagType), fileName));
            }
        }
    }
}