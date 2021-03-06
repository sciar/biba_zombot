using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class SettingsMediator : SceneMenuStateMediator
	{
        [Inject]
        public SettingsView SettingsView { get; set; }

        [Inject]
        public EnableHelpBubblesSignal EnableHelpBubblesSignal { get; set; }

        [Inject]
        public EnableHowToSignal EnableHowToSignal { get; set; }

        [Inject]
		public ResetDeviceSignal ResetDeviceSignal { get; set; }

		[Inject]
		public BibaDevice BibaDevice { get; set; }

        public override SceneMenuStateView View { get { return SettingsView; } }

        public override void SetupSceneDependentMenu ()
        {
			SettingsView.ShowHowToToggle.isOn = BibaDevice.HowToEnabled;
			SettingsView.ShowHelpBubblesToggle.isOn = BibaDevice.HelpBubblesEnabled;
			SettingsView.ResetDeviceSignal = ResetDeviceSignal;
      }

        public override void RegisterSceneDependentSignals ()
        {
            SettingsView.ShowHowToToggle.onValueChanged.AddListener(EnableHowTo);
            SettingsView.ShowHelpBubblesToggle.onValueChanged.AddListener(EnableHelpBubbles);
        }

        public override void UnRegisterSceneDependentSignals ()
        {
            SettingsView.ShowHowToToggle.onValueChanged.RemoveListener(EnableHowTo);
            SettingsView.ShowHelpBubblesToggle.onValueChanged.RemoveListener(EnableHelpBubbles);
        }

        void EnableHowTo(bool status)
        {
            EnableHowToSignal.Dispatch(status);
        }

        void EnableHelpBubbles(bool status)
        {
            EnableHelpBubblesSignal.Dispatch(status);
        }
	}
}