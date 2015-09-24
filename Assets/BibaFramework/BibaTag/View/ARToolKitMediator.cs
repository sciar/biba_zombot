using BibaFramework.BibaMenu;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaTag
{
    public class ARToolKitMediator : Mediator 
    {
        [Inject]
        public ARToolKitView ARToolKitView { get; set; }

        [Inject]
        public TagScannedSignal TagScannedSignal { get; set; }

        [Inject]
        public ToggleScanSignal ToggleScanSignal { get; set; }

        public override void OnRegister ()
        {
            ToggleScanSignal.AddListener(ToggleScan);
            ARToolKitView.ARMarkerFoundSignal.AddListener(ARMarkerFound);
        }

        public override void OnRemove ()
        {
            ToggleScanSignal.RemoveListener(ToggleScan);
            ARToolKitView.ARMarkerFoundSignal.RemoveListener(ARMarkerFound);
        }

        void ToggleScan(bool status)
        {
            ARToolKitView.EnableARController(status);
        }

        void ARMarkerFound(string fileName)
        {
            TagScannedSignal.Dispatch(fileName);
        }
    }
}