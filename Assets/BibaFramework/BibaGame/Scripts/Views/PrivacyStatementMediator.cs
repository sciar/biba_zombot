using BibaFramework.BibaMenu;
using UnityEngine.Events;

namespace BibaFramework.BibaGame
{
    public class PrivacyStatementMediator : SceneMenuStateMediator
    {
        [Inject]
        public PrivacyStatementView PrivacyStatementView { get; set; }

        public override SceneMenuStateView View { get { return PrivacyStatementView; } }
        
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