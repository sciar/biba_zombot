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
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public SessionUpdatedSignal SessionUpdatedSignal { get; set; }

        public override void Execute ()
        {
            if (Status)
            {
				BibaDeviceSession.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType));
				SessionUpdatedSignal.Dispatch ();
            } 
            else
            {
				var indexToRemove = BibaDeviceSession.SelectedEquipments.FindIndex(equip => equip.EquipmentType == BibaEquipmentType);
                if(indexToRemove != -1)
                {
					BibaDeviceSession.SelectedEquipments.RemoveAt(indexToRemove);
                }
            }
        }
    }
}