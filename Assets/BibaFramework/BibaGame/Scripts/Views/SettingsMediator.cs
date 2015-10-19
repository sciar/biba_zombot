using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class SettingsMediator : SceneMenuStateMediator
	{
        [Inject]
        public SettingsView SettingsView { get; set; }

        public override SceneMenuStateView View { get { return SettingsView; } }

        public override void SetupMenu (BaseMenuState menuState)
        {
        }

        public override void RegisterSceneDependentSignals ()
        {
        }

        public override void RemoveSceneDependentSignals ()
        {
        }
	}
}