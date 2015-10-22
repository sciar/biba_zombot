using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class SetSatelliteTagToScanCommand : Command
    {
        [Inject(BibaGameConstants.SATELLITE_TAG_TO_SCAN)]
        public BibaEquipment BibaEquipmentToScan { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        public override void Execute ()
        {
            if (BibaGameModel.SelectedEquipments.Count > 0)
            {
                var rndIndex = Random.Range(0, BibaGameModel.SelectedEquipments.Count);
                BibaEquipmentToScan = new BibaEquipment(BibaGameModel.SelectedEquipments[rndIndex].EquipmentType);
            }
        }
    }
}