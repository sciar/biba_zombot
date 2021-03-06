using System;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	[Serializable]
    public class BibaAchievementSettings
    {
        public List<BibaAchievementConfig> AchievementSettings = new List<BibaAchievementConfig>();
    }

	[Serializable]
    public class BibaAchievementConfig
    {   
        private const string BASIC_ACHIEVEMENT_ID_FORMATTED = "achievement_{0}_{1}";
       
        public BibaEquipmentType EquipmentType;
        public int TimePlayed;
        public string DescriptionSuffix;

        public string DescriptionPrefix { get { return _prefixDict [EquipmentType]; } }

        public virtual string Id {
            get {
                return string.Format(BASIC_ACHIEVEMENT_ID_FORMATTED, EquipmentType.ToString(), TimePlayed.ToString());
            }
        }
			
        private static readonly Dictionary<BibaEquipmentType, string> _prefixDict = new Dictionary<BibaEquipmentType, string>()
        {
            {  BibaEquipmentType.bridge, BibaGameConstants.ACHIEVEMENT_PREFIX_KEY_BRIDGE },
            {  BibaEquipmentType.climber, BibaGameConstants.ACHIEVEMENT_PREFIX_KEY_CLIMBER },
            {  BibaEquipmentType.overhang, BibaGameConstants.ACHIEVEMENT_PREFIX_KEY_OVERHANG },
            {  BibaEquipmentType.slide, BibaGameConstants.ACHIEVEMENT_PREFIX_KEY_SLIDE},
            {  BibaEquipmentType.swing, BibaGameConstants.ACHIEVEMENT_PREFIX_KEY_SWING},
            {  BibaEquipmentType.tube, BibaGameConstants.ACHIEVEMENT_PREFIX_KEY_TUBE },
        };
    }

	[Serializable]
    public class BibaSeasonalAchievementConfig : BibaAchievementConfig
    {   
        private const string SEASONAL_ACHIEVEMENT_ID_FORMATTED = "seasonal_achievement_{0}_{1}_{2}_{3}";
		public DateTime StartDate;
		public DateTime EndDate;

        public override string Id {
            get {
                return string.Format(SEASONAL_ACHIEVEMENT_ID_FORMATTED, EquipmentType.ToString(), TimePlayed.ToString(), StartDate.ToString(), EndDate.ToString());
            }
        }
    }
}