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
        public string DescriptionPrefix;
        public string DescriptionSuffix;
        public string Description { get { return DescriptionPrefix + DescriptionSuffix; } }

        public string Id {
            get {
                return string.Format("{0}_{1}", EquipmentType.ToString(), TimePlayed.ToString());
            }
        }
    }
}