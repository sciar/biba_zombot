using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class IntroMediator : SceneMenuStateMediator
	{
        [Inject]
        public IntroView IntroView { get; set; }

        public override SceneMenuStateView View { get { return IntroView; } }

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