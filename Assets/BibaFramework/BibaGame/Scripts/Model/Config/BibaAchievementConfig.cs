using System;
using UnityEngine;
using BibaFramework.Utility;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class BibaAchievementConfig : ScriptableObject
    {   
        public BibaEquipmentType EquipmentType;
        public int TimePlayed;
        public string DescriptionPrefix { get { return _prefixDict [EquipmentType]; } }
        public string DescriptionSuffix;
        public string Description { get { return DescriptionPrefix + " " + DescriptionSuffix; } }

        public string Id {
            get {
                return string.Format("{0}:{1}_{2}", name, EquipmentType.ToString(), TimePlayed.ToString());
            }
        }

        private static readonly Dictionary<BibaEquipmentType, string> _prefixDict = new Dictionary<BibaEquipmentType, string>()
        {
            {  BibaEquipmentType.bridge, BibaGameConstants.ACHIEVEMENT_PREFIX_BRIDGE },
            {  BibaEquipmentType.climber, BibaGameConstants.ACHIEVEMENT_PREFIX_CLIMBER },
            {  BibaEquipmentType.overhang, BibaGameConstants.ACHIEVEMENT_PREFIX_OVERHANG },
            {  BibaEquipmentType.slide, BibaGameConstants.ACHIEVEMENT_PREFIX_SLIDE},
            {  BibaEquipmentType.swing, BibaGameConstants.ACHIEVEMENT_PREFIX_SWING},
            {  BibaEquipmentType.tube, BibaGameConstants.ACHIEVEMENT_PREFIX_TUBE },
        };
    }
}