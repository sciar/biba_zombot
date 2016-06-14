using BibaFramework.BibaMenu;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public class DownloadMediator : SceneMenuStateMediator
	{
        [Inject]
        public DownloadView DownloadView { get; set; }

        [Inject]
        public DownloadGeoBasedScenesSignal DownloadContentForCurrentLocationSignal { get; set; }

        public override SceneMenuStateView View { get { return DownloadView; } }

        public override void SetupSceneDependentMenu ()
        {
        }

        public override void RegisterSceneDependentSignals ()
        {         
            DownloadView.YesButton.onClick.AddListener(SendDownloadContentSignal);
        }
        
        public override void UnRegisterSceneDependentSignals ()
        {
            DownloadView.YesButton.onClick.RemoveListener(SendDownloadContentSignal);
        }

        void SendDownloadContentSignal()
        {
            DownloadContentForCurrentLocationSignal.Dispatch();
        }
   	}
}