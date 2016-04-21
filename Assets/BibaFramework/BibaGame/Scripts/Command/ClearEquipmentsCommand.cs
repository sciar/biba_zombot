using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class ClearEquipmentsCommand : Command
	{
		[Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

		public override void Execute ()
		{
			BibaSessionModel.SelectedEquipments.Clear ();
		}
	}
}