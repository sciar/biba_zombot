using System;
using BibaFramework.BibaAnalytic;
using strange.extensions.command.impl;

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
			var equipment = BibaGameModel.TotalPlayedEquipments.Find(equip => equip.EquipmentType == BibaEquipmentType);
			if (equipment == null) 
			{
				throw new ArgumentNullException ();
			}
			equipment.Play();

            DataService.WriteGameModel();
            BibaAnalyticService.TrackEquipmentPlayed(equipment.EquipmentType);
        }
    }
}