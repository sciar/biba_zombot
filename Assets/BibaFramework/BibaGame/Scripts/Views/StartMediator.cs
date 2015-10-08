using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class StartMediator : BaseBibaMediator
	{
        [Inject]
        public StartView StartView { get; set; }

        public override BaseBibaView View { get { return StartView; } }

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