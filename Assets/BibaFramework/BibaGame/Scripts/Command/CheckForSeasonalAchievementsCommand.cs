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

                var starDate = new DateTime(DateTime.Now.Year, (int) seasonalConfig.StartDate.x, (int) seasonalConfig.StartDate.y);
                var endDate = new DateTime(DateTime.Now.Year, (int) seasonalConfig.EndDate.x, (int) seasonalConfig.EndDate.y);

                var timePlayedInSeason = equipment.TimesPlayed.Where(date => date >= starDate && date <= endDate).Count();
                return timePlayedInSeason >= seasonalConfig.TimePlayed;
            }
            return false;
        }
    }
}