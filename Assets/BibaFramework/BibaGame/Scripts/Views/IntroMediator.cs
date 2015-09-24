using BibaFramework.BibaMenu;
using BibaFramework.BibaTag;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class IntroMediator : BaseBibaMediator
	{
        [Inject]
        public IntroView IntroView { get; set; }

        public override BaseBibaView View { get { return IntroView; } }

        [Inject]
        public IBibaTagService BibaTagService { get; set; }

        [Inject]
        public TagScanningCompletedSignal TagScanningCompletedSignal { get; set; }

        public override void SetupMenu (BibaMenuState menuState)
        {
            //TagScanningCompletedSignal.AddOnce(ScanCompleted);
           // BibaTagService.StartScanWithCompleteHandler();
        }

        void ScanCompleted()
        {
            IntroView.Text.text = BibaTagService.LastScannedTags.Count.ToString();
        }
   	}
}