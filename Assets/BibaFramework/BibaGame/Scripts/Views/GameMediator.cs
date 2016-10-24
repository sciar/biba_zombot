using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class GameMediator : SceneMenuStateMediator
	{
        [Inject]
        public GameView GameView { get; set; }

        public override SceneMenuStateView View { get { return GameView; } }

        public override void SetupSceneDependentMenu ()
        {
        }

        public override void RegisterSceneDependentSignals ()
        {
        }

        public override void UnRegisterSceneDependentSignals ()
        {
        }
	}
}