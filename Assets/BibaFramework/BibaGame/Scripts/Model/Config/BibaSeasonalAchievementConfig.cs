using UnityEngine;
using System;

namespace BibaFramework.BibaGame
{
    public class BibaSeasonalAchievementConfig : BibaAchievementConfig
    {   
        public int Month;
        public DateTime Date { get { return new DateTime(DateTime.UtcNow.Year, Month, 1); } }
    }
}