using strange.extensions.mediation.impl;
using UnityEngine;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class ARScanStartMediator : BaseObjectMenuStateMediator 
	{
        public override BaseObjectMenuStateView BaseObjectMenuStateView { get { return ARScanStartView; } }

        [Inject]
        public ARScanStartView ARScanStartView { get; set; }
        
        [Inject]
		public IBibaTagService BibaTagService { get; set; }
        
        [Inject]
        public TagScannedSignal TagScannedSignal { get; set; }
        
        [Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }
        
        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        [Inject]
		public TagInitFailedSignal TagInitFailedSignal { get; set; }

        [Inject]
        public LogCameraReminderTimeSignal LogCameraReminderTimeSignal { get; set; }

        [Inject]
        public TagScanCompletedSignal TagScanCompletedSignal { get; set; }

		[Inject]
		public LocalizationService LocalizationService { get; set; }

        private BibaTagType _tagToScan;

        protected override void RegisterMenuStateDependentSignals() 
        { 
            TagScannedSignal.AddListener(TagScanned);
			TagInitFailedSignal.AddListener(TagServiceInitFailed);
            
			ARScanStartView.LocalizationService = LocalizationService;
            SetupTagToScan();
        }

        protected override void UnRegisterMenuStateDependentSignals() 
        {
            TagScannedSignal.RemoveListener(TagScanned);
			TagInitFailedSignal.RemoveListener(TagServiceInitFailed);
        }

        protected override void MenuStateObjectEnabled()
        {
            if (ARScanStartView != null)
            {
                ARScanStartView.SetupCamera();
                BibaTagService.StartScan();
            }
        }

        protected override void MenuStateObjectDisabled()
        {
            if (ARScanStartView != null)
            {
                BibaTagService.StopScan();
                ARScanStartView.DestroyCamera();
            }
        }

        void SetupTagToScan()
        {
			var rndIndex = Random.Range(0, BibaDeviceSession.SelectedEquipments.Count);
			var equipmentToScan = new BibaEquipment(BibaDeviceSession.SelectedEquipments[rndIndex].EquipmentType);
            
            _tagToScan = equipmentToScan.TagType;
            ARScanStartView.SetupTag(_tagToScan);
        }

        void TagScanned(BibaTagType tagType)
        {
            //TODO: implement scanning logic
            Debug.Log(tagType.ToString() + " tag is scanned. We are expecting " + _tagToScan.ToString());
            if (tagType == _tagToScan)
            {
                BibaTagService.StopScan();
                ARScanStartView.CompleteScan(ScanCompleted);
            }
        }

        void ScanCompleted()
        {
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Yes);
			BibaDeviceSession.TagScanned = true;
            TagScanCompletedSignal.Dispatch();

        }

        void TagServiceInitFailed()
        {
            LogCameraReminderTimeSignal.Dispatch();
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.No);
        }
	} 
}