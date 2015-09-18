using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class PrivacyStatementMediator : BaseBibaMediator
    {
        [Inject]
        public PrivacyStatementView PrivacyStatementView { get; set; }
        
        public override BaseBibaView View { get { return PrivacyStatementView; } }
        
        public override void SetupMenu (BibaMenuState menuState)
        {
        }
    }
}