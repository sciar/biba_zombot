using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class PrivacyStatementMediator : BaseSceneBasedMediator
    {
        [Inject]
        public PrivacyStatementView PrivacyStatementView { get; set; }
        
        public override BaseSceneBasedView View { get { return PrivacyStatementView; } }
        
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