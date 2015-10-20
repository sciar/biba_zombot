using System;
using UnityEngine;
using UnityEditor;
using BibaFramework.Utility;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class BibaAchievementConfig : ScriptableObject
    {
        [MenuItem("Biba/Create Achievement Settings")]
        public static void CreateAsset ()
        {
            ScriptableObjectUtility.CreateAsset<BibaAchievementConfig> ();
        }
        
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