using System.Collections.Generic;
using System;

namespace BibaFramework.BibaGame
{
    public class BibaGameModel
    {
        public DateTime LastPlayedTime { get; set; }
        public bool PrivacyPolicyAccepted { get; set; }
        public bool TagEnabled { get; set; }
        public List<BibaEquipment> Equipments { get; set; }

        public BibaGameModel()
        {
            Equipments = new List<BibaEquipment>();
            LastPlayedTime = DateTime.MaxValue;
        }
    }
}