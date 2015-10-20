using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class InactiveMediator : SceneMenuStateMediator
	{
        [Inject]
        public InactiveView InactiveView { get; set; }

        public override SceneMenuStateView View { get { return InactiveView; } }

        public override void SetupMenu (BaseMenuState menuState)
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