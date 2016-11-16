using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class AchievementsMediator : SceneMenuStateMediator
	{
        [Inject]
        public AchievementsView AchievementsView { get; set; }

        [Inject]
        public BibaDeviceSession bibaDeviceSession { get; set; }

        public override SceneMenuStateView View { get { return AchievementsView; } }

        public override void SetupSceneDependentMenu ()
        {
            AchievementsView.bibaDeviceSession = bibaDeviceSession;
        }

        public override void RegisterSceneDependentSignals ()
        {
        }
        
        public override void UnRegisterSceneDependentSignals ()
        {
        }
   	}
}