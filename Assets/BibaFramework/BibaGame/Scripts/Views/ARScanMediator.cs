using BibaFramework.BibaMenu;
using BibaFramework.BibaTag;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class ARScanMediator : SceneMenuStateMediator
    {
        public override SceneMenuStateView View { get { return TestARView; } }

        [Inject]
        public ARScanView TestARView { get; set; }
        
        [Inject]
        public IBibaTagService BibaTagService { get; set; }

        [Inject]
        public TagScannedSignal TagScannedSignal { get; set; }

        public override void SetupMenu (BaseMenuState menuState)
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
            //TODO: implement scanning logic
            Debug.Log(tagType.ToString() + " tag is scanned.");
        }
    }
}