using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class InactiveMediator : SceneMenuStateMediator
	{
        [Inject]
        public InactiveView InactiveView { get; set; }

        [Inject]
		public BibaSystem BibaSystem { get; set; }

		[Inject]
		public StartNewSessionSignal StartNewSessionSignal { get; set; }

        public override SceneMenuStateView View { get { return InactiveView; } }

        public override void SetupSceneDependentMenu ()
        {
			var lastPlayedTimeLocal = BibaSystem.LastPlayedTime.ToLocalTime();
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