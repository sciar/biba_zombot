using BibaFramework.BibaMenu;
using UnityEngine.Events;

namespace BibaFramework.BibaGame
{
    public class PrivacyStatementMediator : SceneMenuStateMediator
    {
        [Inject]
        public PrivacyStatementView PrivacyStatementView { get; set; }
        
		[Inject]
		public OpenAboutBibaURLSignal OpenAboutBibaURLSignal { get; set; }

        public override SceneMenuStateView View { get { return PrivacyStatementView; } }
        
        public override void SetupMenu (BaseMenuState menuState)
        {
        }

        public override void RegisterSceneDependentSignals ()
        {
			PrivacyStatementView.OpenURLButton.onClick.AddListener(OpenUrlButtonClicked);
        }
        
        public override void UnRegisterSceneDependentSignals ()
		{
			PrivacyStatementView.OpenURLButton.onClick.RemoveListener(OpenUrlButtonClicked);
        }

		void OpenUrlButtonClicked()
		{
			OpenAboutBibaURLSignal.Dispatch();
		}
    }
}