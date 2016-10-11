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
		public TagScanCompletedSignal TagScanCompletedSignal { get; set; }
        
        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        [Inject]
		public TagInitFailedSignal TagInitFailedSignal { get; set; }

		[Inject]
		public LocalizationService LocalizationService { get; set; }

		[Inject]
		public StartTagScanSignal StartTagScanSignal { get; set; }

		[Inject]
		public SetTagToScanAtViewSignal SetTagToScanAtViewSignal { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public ToggleARCameraSignal ToggleARCameraSignal { get; set; }

        protected override void RegisterMenuStateDependentSignals() 
        { 
			TagScanCompletedSignal.AddListener(TagScanCompleted);
			TagInitFailedSignal.AddListener(TagServiceInitFailed);
            
			ARScanStartView.LocalizationService = LocalizationService;
        }

        protected override void UnRegisterMenuStateDependentSignals() 
        {
			TagScanCompletedSignal.RemoveListener(TagScanCompleted);
			TagInitFailedSignal.RemoveListener(TagServiceInitFailed);
        }

        protected override void MenuStateObjectEnabled()
        {
			SetupTagToScan();
			ToggleARCameraSignal.Dispatch (true);
        }

		protected override void MenuStateObjectDisabled()
		{
			ToggleARCameraSignal.Dispatch (false);
		}

		void SetupTagToScan()
		{
			SetTagToScanAtViewSignal.AddListener (SetTagToScanAtView);
			StartTagScanSignal.Dispatch ();
		}

		void SetTagToScanAtView()
		{
			SetTagToScanAtViewSignal.RemoveListener (SetTagToScanAtView);
			ARScanStartView.SetupTag(BibaDeviceSession.TagToScan);
		}

		void TagScanCompleted()
		{
			ARScanStartView.CompleteScan(ScanCompletedAnimationCompleted);
		}

        void ScanCompletedAnimationCompleted()
        {
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Yes);
        }

        void TagServiceInitFailed()
        {
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.No);
        }
	} 
}