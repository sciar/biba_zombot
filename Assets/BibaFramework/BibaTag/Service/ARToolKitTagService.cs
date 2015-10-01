﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BibaFramework.BibaMenu;
using BibaFramework.Utility;

namespace BibaFramework.BibaTag
{
    public class ARToolKitTagService : IBibaTagService
    {
        [Inject]
        public ToggleScanSignal ToggleScanSignal { get; set; }

        [Inject]
        public TagScannedSignal TagScannedSignal { get; set; }

        private HashSet<BibaTag> _lastScannedTags;
        public HashSet<BibaTag> LastScannedTags {
            get {
                if(_lastScannedTags == null)
                {
                    TagScannedSignal.AddListener(OnTagScanned);
                    _lastScannedTags = new HashSet<BibaTag>();
  
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
            if(Enum.IsDefined(typeof(BibaTag), fileName))
            {
                LastScannedTags.Add((BibaTag)Enum.Parse(typeof(BibaTag), fileName));
            }
        }
    }
}