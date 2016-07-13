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
		public BibaAccount BibaAccount { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IAnalyticService BibaAnalyticService { get; set; }

        public override void Execute ()
        {
			var equipment = BibaAccount.SelectedProfile.PlayedEquipments.Find(equip => equip.EquipmentType == BibaEquipmentType);
			if (equipment == null) 
			{
				throw new ArgumentNullException ();
			}
			equipment.Play();

			DataService.Save();
            BibaAnalyticService.TrackEquipmentPlayed(equipment.EquipmentType);
        }
    }
}