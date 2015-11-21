using System.Collections.Generic;
using System;

namespace BibaFramework.BibaGame
{
    public class BibaGameModel
    {
        //Persisted Player Data
        public DateTime LastPlayedTime { get; set; }
        public DateTime LastChartBoostTime { get; set; }
        public DateTime LastCameraReminderTime { get; set; }

        public List<BibaEquipment> SelectedEquipments { get; set; }
        public List<BibaEquipment> TotalPlayedEquipments { get; set; }
        public List<BibaAchievement> CompletedAchievements { get; set; }

        public bool PrivacyEnabled { get; set; }
        public bool TagEnabled { get; set; }
        public bool HowToEnabled { get; set; }
        public bool HelpBubblesEnabled { get; set; }

        public double HighScore { get; set; }

        public BibaGameModel()
        {
            Reset();
        }

        public void Reset()
        {
            LastPlayedTime = DateTime.MaxValue;
            LastChartBoostTime = DateTime.MinValue;
            LastCameraReminderTime = DateTime.MinValue;

            SelectedEquipments = new List<BibaEquipment>();
            
            TotalPlayedEquipments = new List<BibaEquipment>() {
                new BibaEquipment(BibaEquipmentType.bridge),
                new BibaEquipment(BibaEquipmentType.climber),
                new BibaEquipment(BibaEquipmentType.overhang),
                new BibaEquipment(BibaEquipmentType.slide),
                new BibaEquipment(BibaEquipmentType.swing),
                new BibaEquipment(BibaEquipmentType.tube),
            };
            
            CompletedAchievements = new List<BibaAchievement>();

            PrivacyEnabled = false;
            TagEnabled = false;
            HowToEnabled = true;
            HelpBubblesEnabled = true;

            HighScore = 0;
        }
    }
}