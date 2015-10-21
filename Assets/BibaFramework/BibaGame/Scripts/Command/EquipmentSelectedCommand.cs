using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectedCommand : Command
    {
        [Inject]
        public BibaEquipmentType BibaEquipmentType  { get; set; }

        [Inject]
        public bool Status { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
            if (Status)
            {
                BibaGameModel.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType));
                BibaGameModel.TotalPlayedEquipments.Find(equip => equip.EquipmentType == BibaEquipmentType).TimeSelected++;
            } 
            else
            {
                var indexToRemove = BibaGameModel.SelectedEquipments.FindIndex(equip => equip.EquipmentType == BibaEquipmentType);
                if(indexToRemove != -1)
                {
                    BibaGameModel.SelectedEquipments.RemoveAt(indexToRemove);
                    BibaGameModel.TotalPlayedEquipments.Find(equip => equip.EquipmentType == BibaEquipmentType).TimeSelected--;
                }
            }

            DataService.WriteGameModel();
        }
    }
}