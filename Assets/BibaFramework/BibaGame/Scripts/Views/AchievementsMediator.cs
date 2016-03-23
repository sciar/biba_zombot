using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class AchievementsMediator : SceneMenuStateMediator
	{
        [Inject]
        public AchievementsView AchievementsView { get; set; }

        public override SceneMenuStateView View { get { return AchievementsView; } }

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