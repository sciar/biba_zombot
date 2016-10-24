using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class InactiveMediator : SceneMenuStateMediator
	{
        [Inject]
        public InactiveView InactiveView { get; set; }

        [Inject]
		public BibaDevice BibaDevice { get; set; }

		[Inject]
		public StartNewSessionSignal StartNewSessionSignal { get; set; }

        public override SceneMenuStateView View { get { return InactiveView; } }

        public override void SetupSceneDependentMenu ()
        {
			var lastPlayedTimeLocal = BibaDevice.LastPlayedTime.ToLocalTime();
			InactiveView.Text.text = string.Format(InactiveView.Text.text, lastPlayedTimeLocal.ToShortDateString(), lastPlayedTimeLocal.ToShortTimeString());
        }

        public override void RegisterSceneDependentSignals ()
        {
			InactiveView.NoButton.onClick.AddListener (StartNewSession);
        }
        
        public override void UnRegisterSceneDependentSignals ()
        {
			InactiveView.NoButton.onClick.RemoveListener (StartNewSession);
        }

		void StartNewSession()
		{
			StartNewSessionSignal.Dispatch ();
		}
	}
}