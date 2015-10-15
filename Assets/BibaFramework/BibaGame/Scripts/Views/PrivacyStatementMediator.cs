using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class PrivacyStatementMediator : SceneMenuStateMediator
    {
        [Inject]
        public PrivacyStatementView PrivacyStatementView { get; set; }
        
        public override SceneMenuStateView View { get { return PrivacyStatementView; } }
        
        public override void SetupMenu (BaseMenuState menuState)
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