using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class GameMediator : BaseBibaMediator
	{
        [Inject]
        public GameView GameView { get; set; }

        public override BaseBibaView View { get { return GameView; } }

        public override void SetupMenu (BibaMenuState menuState)
        {
        }
	}
}