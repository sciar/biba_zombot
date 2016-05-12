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
		public List<BibaEquipment> SelectedEquipments { get; set; }

        public bool PrivacyEnabled { get; set; }
        public bool HowToEnabled { get; set; }
        public bool HelpBubblesEnabled { get; set; }
		public bool TagEnabled { get; set; }

		public DateTime LastPlayedTime { get; set; }
		public DateTime LastChartBoostTime { get; set; }
		public DateTime LastCameraReminderTime { get; set; }

		public SystemLanguage LanguageOverwrite { get; set; }

		public int FrameworkVersion { get; set; }
        public double HighScore { get; set; }

		public int Points { get; set; }
		public List<string> CompletedPointsEvent { get; set; }

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
            
			SelectedEquipments = new List<BibaEquipment> ();
            CompletedAchievements = new List<BibaAchievement>();

			LanguageOverwrite = SystemLanguage.Unknown;
            PrivacyEnabled = false;
            HowToEnabled = true;
            HelpBubblesEnabled = true;
			TagEnabled = false;

			LastPlayedTime = DateTime.MaxValue;
			LastChartBoostTime = DateTime.MinValue;
			LastCameraReminderTime = DateTime.MinValue;

            HighScore = 0;
			Points = 0;
			CompletedPointsEvent = new List<string> ();
        }
    }
}