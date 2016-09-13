using System;
using strange.extensions.command.impl;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
    public class EquipmentPlayedCommand : Command
    {
        [Inject]
        public BibaEquipmentType BibaEquipmentType { get; set; }

		[Inject]
		public BibaProfile BibaProfile { get; set; }

		[Inject]
		public BibaDevice BibaDevice { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

		[Inject]
		public IDeviceAnalyticService DeviceAnalyticService { get; set; }

        public override void Execute ()
        {
			var sessionEquipment = BibaProfile.BibaProfileSession.SessionEquipments.Find(equip => equip.EquipmentType == BibaEquipmentType);
			if (sessionEquipment == null) 
			{
				throw new ArgumentNullException ();
			}
			sessionEquipment.Play();

			var deviceEquipment = BibaDevice.TotalEquipments.Find(equip => equip.EquipmentType == BibaEquipmentType);
			if (deviceEquipment == null) 
			{
				throw new ArgumentNullException ();
			}
			deviceEquipment.Play();

			DataService.Save();

			DeviceAnalyticService.TrackEquipmentPlayed (BibaEquipmentType);
        }
    }
}