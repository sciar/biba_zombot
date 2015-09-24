using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using System.IO;
using System;

namespace BibaFramework.BibaTag
{
    public class ARToolKitView : View 
    {
        private const float PATTERN_WIDTH = 0.1f;

        public GameObject ARMarkerPrefab;
        public string ARPatternResourceFolderPath;
        public Transform ARTargetContainer;

        public Signal<string> ARMarkerFoundSignal = new Signal<string>();

        private ARController _arController;

        void Start()
        {
            _arController = GetComponentInChildren<ARController>();
            SetupMarkerPatterns();
        }

        void SetupMarkerPatterns()
        {
            var patternFiles = Resources.LoadAll<TextAsset>(ARPatternResourceFolderPath);

            for(int i = 0; i < patternFiles.Length; i++)
            {
                var file = patternFiles[i];

                //Set up ARMarker
                var arMarker = _arController.gameObject.AddComponent<ARMarker>();
                arMarker.MarkerType = MarkerType.Square;
                arMarker.PatternFilenameIndex = i;
                arMarker.PatternFilename = file.name;
                arMarker.PatternContents = file.text;
                arMarker.PatternWidth = PATTERN_WIDTH;
                arMarker.Tag = file.name;
                arMarker.Load();

                //Set up ARTrackedObject
                var arTrackedObject = Instantiate(ARMarkerPrefab).GetComponent<ARTrackedObject>();
                arTrackedObject.transform.parent = ARTargetContainer;
                arTrackedObject.MarkerTag = arMarker.Tag;
                arTrackedObject.eventReceiver = gameObject;
            }

            //Have to add the AROrigin for the ArTrackedObject eventReceiver assignment.
            ARTargetContainer.gameObject.AddComponent<AROrigin>();
        }

        public void EnableARController(bool status)
        {
            if (status)
            {
                _arController.StartAR();
            }
            else
            {
                _arController.StopAR();
            }
        }

        void OnMarkerLost(ARMarker marker)
        {
            //Debug.Log(marker.PatternFilename + "lost");
        }
        
        void OnMarkerFound(ARMarker marker)
        {
            ARMarkerFoundSignal.Dispatch(marker.PatternFilename);
            //Debug.Log(marker.PatternFilename + "found");
        }
    }
}
