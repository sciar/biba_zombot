using System.Collections.Generic;
using System;

namespace BibaFramework.BibaGame
{
    public class BibaGameModel
    {
        //Player Data
        public DateTime LastPlayedTime { get; set; }
        public bool PrivacyPolicyAccepted { get; set; }
        public bool TagEnabled { get; set; }
        public bool ShowHowTo { get; set; }
        public bool ShowHelpBubble { get; set; }
        public List<BibaEquipment> SelectedEquipments { get; set; }
        public List<BibaEquipment> TotalPlayedEquipments { get; set; }


        public BibaGameModel()
        {
            SelectedEquipments = new List<BibaEquipment>();

            TotalPlayedEquipments = new List<BibaEquipment>() {
                new BibaEquipment(BibaEquipmentType.bridge),
                new BibaEquipment(BibaEquipmentType.climber),
                new BibaEquipment(BibaEquipmentType.overhang),
                new BibaEquipment(BibaEquipmentType.slide),
                new BibaEquipment(BibaEquipmentType.swing),
                new BibaEquipment(BibaEquipmentType.tube),
            };

            LastPlayedTime = DateTime.MaxValue;

            ShowHowTo = true;
            ShowHelpBubble = true;
        }
    }
}