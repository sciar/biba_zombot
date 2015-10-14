using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class IntroMediator : BaseSceneBasedMediator
	{
        [Inject]
        public IntroView IntroView { get; set; }

        public override BaseSceneBasedView View { get { return IntroView; } }

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