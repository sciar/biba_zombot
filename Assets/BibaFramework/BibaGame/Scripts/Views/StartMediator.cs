using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class StartMediator : BaseSceneBasedMediator
	{
        [Inject]
        public StartView StartView { get; set; }

        public override BaseSceneBasedView View { get { return StartView; } }

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