using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class ARScanContextStartCommand : Command
    {
        [Inject]
        public BibaEquipment BibaEquipmentToScan { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        public override void Execute ()
        {
            if (BibaGameModel.Equipments.Count > 0)
            {
                var rndIndex = Random.Range(0, BibaGameModel.Equipments.Count);
                BibaEquipmentToScan = new BibaEquipment(BibaGameModel.Equipments[rndIndex].EquipmentType);
            }
        }
    }
}