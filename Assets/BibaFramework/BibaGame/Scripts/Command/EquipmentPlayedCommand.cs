using strange.extensions.command.impl;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
    public class EquipmentPlayedCommand : Command
    {
        [Inject]
        public BibaEquipmentType BibaEquipmentType { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IAnalyticService BibaAnalyticService { get; set; }

        public override void Execute ()
        {
            var equipmentIndex = BibaGameModel.TotalPlayedEquipments.FindIndex(equip => equip.EquipmentType == BibaEquipmentType);

            BibaEquipment equipment;
            if (equipmentIndex == -1)
            {
                equipment = new BibaEquipment(BibaEquipmentType);
                BibaGameModel.TotalPlayedEquipments.Add(equipment);
            }
            else
            {
                equipment = BibaGameModel.TotalPlayedEquipments[equipmentIndex];
            }

            equipment.TimePlayed++;
            DataService.WriteGameModel();

            BibaAnalyticService.TrackEquipmentPlayed(equipment.EquipmentType);
        }
    }
}