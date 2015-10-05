using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectToggledCommand : Command
    {
        [Inject]
        public BibaEquipmentType BibaEquipmentType { get; set; }

        [Inject]
        public bool Status { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        public override void Execute ()
        {
            if (Status)
            {
                var bibaEquipment = new BibaEquipment(BibaEquipmentType);
                BibaGameModel.EquipmentDict[BibaEquipmentType] = bibaEquipment;
            } 
            else
            {
                BibaGameModel.EquipmentDict.Remove(BibaEquipmentType);
            }

            Debug.Log(string.Format("Equipment: {0} - {1}", BibaEquipmentType.ToString(), Status ? "Selected" : "Deselected"));
        }
    }
}

