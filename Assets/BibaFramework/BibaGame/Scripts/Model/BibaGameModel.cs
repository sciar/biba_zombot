using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class BibaGameModel
    {
        public bool PrivacyPolicyAccepted { get; set; }
        public List<BibaEquipment> Equipments { get; set; }

        public BibaGameModel()
        {
            Equipments = new List<BibaEquipment>();
        }
    }
}