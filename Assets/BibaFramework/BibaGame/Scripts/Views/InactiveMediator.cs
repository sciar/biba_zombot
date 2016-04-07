using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class InactiveMediator : SceneMenuStateMediator
	{
        [Inject]
        public InactiveView InactiveView { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        public override SceneMenuStateView View { get { return InactiveView; } }

        public override void SetupSceneDependentMenu ()
        {
            var lastPlayedTimeLocal = BibaGameModel.LastPlayedTime.ToLocalTime();
			InactiveView.Text.text = string.Format(InactiveView.Text.text, lastPlayedTimeLocal.ToShortDateString(), lastPlayedTimeLocal.ToShortTimeString());
        }

        public override void RegisterSceneDependentSignals ()
        {
        }
        
        public override void UnRegisterSceneDependentSignals ()
        {
        }
	}
}