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
        public BibaGameModel BibaGameModel { get; set; }

        public override SceneMenuStateView View { get { return SettingsView; } }

        public override void SetupMenu (BaseMenuState menuState)
        {
            SettingsView.ShowHowToToggle.isOn = BibaGameModel.HowToEnabled;
            SettingsView.ShowHelpBubblesToggle.isOn = BibaGameModel.HelpBubblesEnabled;
        }

        public override void RegisterSceneDependentSignals ()
        {
            SettingsView.ShowHowToToggle.onValueChanged.AddListener(EnableHowTo);
            SettingsView.ShowHelpBubblesToggle.onValueChanged.AddListener(EnableHelpBubbles);
        }

        public override void RemoveSceneDependentSignals ()
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