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
		public BibaSession BibaSession { get; set; }

        public override void Execute ()
        {
            if (Status)
            {
				BibaSession.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType));
            } 
            else
            {
				var indexToRemove = BibaSession.SelectedEquipments.FindIndex(equip => equip.EquipmentType == BibaEquipmentType);
                if(indexToRemove != -1)
                {
					BibaSession.SelectedEquipments.RemoveAt(indexToRemove);
                }
            }
        }
    }
}