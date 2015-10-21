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
        public string Description;

        public string Id {
            get {
                return string.Format("{0}_{1}", EquipmentType.ToString(), TimePlayed.ToString());
            }
        }
    }
}