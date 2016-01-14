using UnityEngine;
using System;

namespace BibaFramework.BibaGame
{
    public class BibaSeasonalAchievementConfig : BibaAchievementConfig
    {   
        public Vector2 StartDate = new Vector2(1, 1);
        public Vector2 EndDate  = new Vector2(1, 31);

        public override string Id {
            get {
                return GenerateId(EquipmentType, TimePlayed, StartDate, EndDate);
            }
        }

        public static string GenerateId(BibaEquipmentType equipmentType, int timePlayed, Vector2 startDate, Vector2 endDate)
        {
            return string.Format(BibaGameConstants.SEASONAL_ACHIEVEMENT_ID_FORMATTED, equipmentType.ToString(), timePlayed.ToString(), startDate.ToString(), endDate.ToString());
        }
    }
}