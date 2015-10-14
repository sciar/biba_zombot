using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class GameMediator : BaseSceneBasedMediator
	{
        [Inject]
        public GameView GameView { get; set; }

        public override BaseSceneBasedView View { get { return GameView; } }

        public override void SetupMenu (BibaMenuState menuState)
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