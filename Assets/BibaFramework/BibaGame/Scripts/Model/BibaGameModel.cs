using System.Collections.Generic;
using System;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaGameModel
    {
        //Persisted Player Data
        public List<BibaEquipment> TotalPlayedEquipments { get; set; }
        public List<BibaAchievement> CompletedAchievements { get; set; }

        public bool PrivacyEnabled { get; set; }
        public bool HowToEnabled { get; set; }
        public bool HelpBubblesEnabled { get; set; }

		public SystemLanguage LanguageOverwrite { get; set; }

		public int FrameworkVersion { get; set; }
        public double HighScore { get; set; }

        public BibaGameModel()
        {
            Reset();
        }

        public void Reset()
        {
            TotalPlayedEquipments = new List<BibaEquipment>() {
                new BibaEquipment(BibaEquipmentType.bridge),
                new BibaEquipment(BibaEquipmentType.climber),
                new BibaEquipment(BibaEquipmentType.overhang),
                new BibaEquipment(BibaEquipmentType.slide),
                new BibaEquipment(BibaEquipmentType.swing),
                new BibaEquipment(BibaEquipmentType.tube),
            };
            
            CompletedAchievements = new List<BibaAchievement>();

			LanguageOverwrite = SystemLanguage.Unknown;
            PrivacyEnabled = false;
            HowToEnabled = true;
            HelpBubblesEnabled = true;

            HighScore = 0;
        }
    }
}