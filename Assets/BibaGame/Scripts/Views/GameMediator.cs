using BibaFramework.BibaMenu;

namespace BibaGame
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