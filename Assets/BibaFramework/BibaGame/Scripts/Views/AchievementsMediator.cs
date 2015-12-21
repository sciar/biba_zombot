using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class AchievementsMediator : SceneMenuStateMediator
	{
        [Inject]
        public AchievementsView AchievementsView { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        public override SceneMenuStateView View { get { return AchievementsView; } }

        public override void SetupSceneDependentMenu ()
        {
            AchievementsView.AchievementText.text = BibaGameModel.CompletedAchievements.Count.ToString() + " achievements completed.";
        }

        public override void RegisterSceneDependentSignals ()
        {
        }
        
        public override void UnRegisterSceneDependentSignals ()
        {
        }
   	}
}