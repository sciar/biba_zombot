using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class InactiveMediator : BaseBibaMediator
	{
        [Inject]
        public InactiveView InactiveView { get; set; }

        public override BaseBibaView View { get { return InactiveView; } }

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