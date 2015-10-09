using BibaFramework.BibaMenu;
using BibaFramework.BibaTag;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class TestARMediator : BaseBibaMediator
    {
        public override BaseBibaView View { get { return TestARView; } }

        [Inject]
        public TestARView TestARView { get; set; }
        
        [Inject]
        public IBibaTagService BibaTagService { get; set; }

        [Inject]
        public TagScannedSignal TagScannedSignal { get; set; }

        public override void SetupMenu (BibaMenuState menuState)
        {
        }

        public override void RegisterSceneDependentSignals ()
        {
            BibaTagService.StartScan();
            TagScannedSignal.AddListener(TagScanned);
        }
        
        public override void RemoveSceneDependentSignals ()
        {
            TagScannedSignal.RemoveListener(TagScanned);
        }

        void TagScanned(BibaTagType tagType)
        {
            BibaTagService.StopScan();
            Debug.Log(tagType.ToString() + " tag is scanned.");
        }
    }
}