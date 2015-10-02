using BibaFramework.BibaTag;
using System.Linq;
using System;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class BibaGameModel
    {
        public Dictionary<BibaEquipmentType, BibaEquipment> EquipmentDict { get; set; }

        public BibaGameModel()
        {
            EquipmentDict = new Dictionary<BibaEquipmentType, BibaEquipment>();
        }
    }
}