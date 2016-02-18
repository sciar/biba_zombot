using System.Linq;
using System.Collections.Generic;
using System;

namespace BibaFramework.BibaGame
{
    public class CheckForSeasonalAchievementsCommand : CheckForAchievementsCommand
    {
        protected override bool IsAchievementCompleted(BibaEquipment equipment, BibaAchievementConfig config)
        {
            if (config is BibaSeasonalAchievementConfig)
            {
                var seasonalConfig = (BibaSeasonalAchievementConfig)config;
                var timePlayedInSeason = equipment.TimesPlayed.Where(date => date >= seasonalConfig.StartDate && date <= seasonalConfig.EndDate).Count();
                return timePlayedInSeason >= seasonalConfig.TimePlayed;
            }
            return false;
        }
    }
}