using BibaFramework.BibaMenu;
using BibaFramework.BibaTag;
using System.Linq;
using System.Text;

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
            TestARView.ResetScanButton.onClick.AddListener(ResetScan);
            TagScannedSignal.AddListener(TagScanned);
        }
        
        public override void RemoveSceneDependentSignals ()
        {
            TestARView.ResetScanButton.onClick.RemoveListener(ResetScan);
            TagScannedSignal.AddListener(TagScanned);
        }

        void ResetScan()
        {
            TestARView.Text.text = string.Empty;
            BibaTagService.StartScan();
        }

        void TagScanned(string scannedTag)
        {
            StringBuilder builder = new StringBuilder();
            foreach(var tag in BibaTagService.LastScannedTags)
            {
                builder.Append(tag.ToString() + ", ");
            }

            TestARView.Text.text = builder.ToString();
        }
    }
}