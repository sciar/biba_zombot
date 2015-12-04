using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class InactiveMediator : SceneMenuStateMediator
	{
        private const string INACTIVE_MSG = "You have been inactive since {0} {1}. Are you still at the same playground?";

        [Inject]
        public InactiveView InactiveView { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        public override SceneMenuStateView View { get { return InactiveView; } }

        public override void SetupMenu (BaseMenuState menuState)
        {
            var lastPlayedTimeLocal = BibaGameModel.LastPlayedTime.ToLocalTime();
            InactiveView.Text.text = string.Format(INACTIVE_MSG, lastPlayedTimeLocal.ToShortDateString(), lastPlayedTimeLocal.ToShortTimeString());
        }

        public override void RegisterSceneDependentSignals ()
        {
        }
        
        public override void UnRegisterSceneDependentSignals ()
        {
        }
	}
}