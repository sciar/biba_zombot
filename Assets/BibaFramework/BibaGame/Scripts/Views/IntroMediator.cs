using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class IntroMediator : BaseBibaMediator
	{
        [Inject]
        public IntroView IntroView { get; set; }

        public override BaseBibaView View { get { return IntroView; } }

        public override void SetupMenu (BibaMenuState menuState)
        {
            var policyAccepted = ((IntroMenuState)menuState).PrivatePolicyAccepted;
            IntroView.ScanButton.gameObject.SetActive(policyAccepted);
            IntroView.TitleText.text = policyAccepted ? "Intro_Policy_Accepted" : "Intro";
        }
   	}
}