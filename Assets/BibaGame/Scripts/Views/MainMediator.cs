using BibaFramework.BibaMenu;

namespace BibaGame
{
	public class MainMediator : BaseBibaMediator
	{
        [Inject]
        public MainView MainView { get; set; }

        public override BaseBibaView View { get { return MainView; } }

        public override void SetupMenu (BibaMenuState menuState)
        {
        }
	}
}