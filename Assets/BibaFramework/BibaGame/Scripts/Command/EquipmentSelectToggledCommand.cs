using strange.extensions.command.impl;

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
        }
    }
}

