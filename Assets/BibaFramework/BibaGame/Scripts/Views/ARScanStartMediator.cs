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
        public TagFoundSignal TagFoundSignal { get; set; }
        
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

		[Inject]
		public SetTagToScanSignal SetTagToScanSignal { get; set; }

		[Inject]
		public SetTagToScanAtViewSignal SetTagToScanAtViewSignal { get; set; }

        private BibaTagType _tagToScan;

        protected override void RegisterMenuStateDependentSignals() 
        { 
            TagFoundSignal.AddListener(TagFound);
			TagInitFailedSignal.AddListener(TagServiceInitFailed);
            
			ARScanStartView.LocalizationService = LocalizationService;
            SetupTagToScan();
        }

        protected override void UnRegisterMenuStateDependentSignals() 
        {
            TagFoundSignal.RemoveListener(TagFound);
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
			SetTagToScanAtViewSignal.AddListener (SetTagToScanAtView);
			SetTagToScanSignal.Dispatch ();
		}

		void SetTagToScanAtView(BibaEquipment equipment)
		{
			SetTagToScanAtViewSignal.RemoveListener (SetTagToScanAtView);

			_tagToScan = equipment.TagType;
			ARScanStartView.SetupTag(_tagToScan);
		}

        void TagFound(BibaTagType tagType)
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
			TagScanCompletedSignal.Dispatch();
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Yes);
        }

        void TagServiceInitFailed()
        {
            LogCameraReminderTimeSignal.Dispatch();
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.No);
        }
	} 
}