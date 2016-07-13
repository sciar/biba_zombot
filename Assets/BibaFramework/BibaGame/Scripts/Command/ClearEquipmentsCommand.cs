using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class ClearEquipmentsCommand : Command
	{
		[Inject]
		public BibaSession BibaSession { get; set; }

		public override void Execute ()
		{
			BibaSession.SelectedEquipments.Clear ();
		}
	}
}