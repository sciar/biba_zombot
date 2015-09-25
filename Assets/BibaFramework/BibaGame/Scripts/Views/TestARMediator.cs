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
       
        public override void OnRegister ()
        {
            base.OnRegister ();
            TestARView.StartScanButton.onClick.AddListener(StartScan);
            TestARView.StopScanButton.onClick.AddListener(StopScan);

            TagScannedSignal.AddListener(TagScanned);
        }

        public override void OnRemove ()
        {
            base.OnRemove ();
            TestARView.StartScanButton.onClick.RemoveListener(StartScan);
            TestARView.StopScanButton.onClick.RemoveListener(StopScan);

            TagScannedSignal.AddListener(TagScanned);
        }

        void StartScan()
        {
            BibaTagService.StartScan();
        }

        void StopScan()
        {
            BibaTagService.StopScan();
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