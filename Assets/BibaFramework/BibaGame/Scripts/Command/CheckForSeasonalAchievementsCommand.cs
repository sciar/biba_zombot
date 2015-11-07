using System.Linq;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class CheckForSeasonalAchievementsCommand : CheckForAchievementsCommand
    {
        protected override IEnumerable<BibaAchievementConfig> AchievementsToCheck {
            get {
                return base.AchievementsToCheck.Where(config => config is BibaSeasonalAchievementConfig);
            }
        }

        protected override bool IsAchievementCompleted(BibaEquipment equipment, BibaAchievementConfig config)
        {
            var seasonalConfig = (BibaSeasonalAchievementConfig)config;
            var timePlayedInSeason = equipment.TimesPlayed.Where(date => date >= seasonalConfig.StartDate && date <= seasonalConfig.EndDate).Count();
            return timePlayedInSeason >= seasonalConfig.TimePlayed;
        }
    }
}