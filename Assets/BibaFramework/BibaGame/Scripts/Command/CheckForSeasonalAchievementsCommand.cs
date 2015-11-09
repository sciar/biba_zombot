using System.Linq;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class CheckForSeasonalAchievementsCommand : CheckForAchievementsCommand
    {
        protected override bool IsAchievementCompleted(BibaEquipment equipment, BibaAchievementConfig config)
        {
            if (config is BibaSeasonalAchievementConfig)
            {
                var seasonalConfig = (BibaSeasonalAchievementConfig)config;
                var timePlayedInSeason = equipment.TimesPlayed.Where(date => date.Month == seasonalConfig.Date.Month).Count();
                return timePlayedInSeason >= seasonalConfig.TimePlayed;
            }
            return false;
        }
    }
}