using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
    public class CheckForAchievementsCommand : Command
    {
        [Inject]
        public AchievementService AchievementService { get; set; }

        public override void Execute ()
        {
            AchievementService.CheckAndCompleteAchievements();
        }
    }
}