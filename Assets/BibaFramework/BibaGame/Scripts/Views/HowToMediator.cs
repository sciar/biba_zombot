using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class HowToMediator : SceneMenuStateMediator
	{
        [Inject]
        public HowToView HowToView { get; set; }

        public override SceneMenuStateView View { get { return HowToView; } }

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