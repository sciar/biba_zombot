using BibaFramework.BibaMenu;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class ARScanMediator : SceneMenuStateMediator
    {
        public override SceneMenuStateView View { get { return ARScanView; } }

        [Inject]
        public ARScanView ARScanView { get; set; }
        
        [Inject]
        public IBibaTagService BibaTagService { get; set; }

        [Inject]
        public TagScannedSignal TagScannedSignal { get; set; }

        [Inject]
        public BibaSessionModel BibaSessionModel { get; set; }

        [Inject]
        public TagServiceInitFailedSignal TagServiceInitFailedSignal { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        private BibaTagType _tagToScan;

        public override void SetupMenu (BaseMenuState menuState)
        {
        }

        public override void RegisterSceneDependentSignals ()
        {
            TagServiceInitFailedSignal.AddListener(TagServiceInitFailed);
            TagScannedSignal.AddListener(TagScanned);

            SetupTagToScan();
            BibaTagService.StartScan();
        }
        
        public override void UnRegisterSceneDependentSignals ()
        {
            TagServiceInitFailedSignal.RemoveListener(TagServiceInitFailed);
            TagScannedSignal.RemoveListener(TagScanned);
        }

        void TagServiceInitFailed()
        {
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.No);
        }

        void SetupTagToScan()
        {
            var rndIndex = Random.Range(0, BibaGameModel.SelectedEquipments.Count);
            var equipmentToScan = new BibaEquipment(BibaGameModel.SelectedEquipments[rndIndex].EquipmentType);

            _tagToScan = equipmentToScan.TagType;
            ARScanView.SetupTag(_tagToScan);
        }

        void TagScanned(BibaTagType tagType)
        {
            //TODO: implement scanning logic
            Debug.Log(tagType.ToString() + " tag is scanned. We are expecting " + _tagToScan.ToString());
            if (tagType == _tagToScan)
            {
                SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Yes);
                BibaSessionModel.TagScanned = true;
            }
        }
    }
}