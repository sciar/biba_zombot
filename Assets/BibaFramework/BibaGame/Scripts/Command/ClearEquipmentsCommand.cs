using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class ClearEquipmentsCommand : Command
	{
		[Inject]
		public BibaGameModel BibaGameModel { get; set; }

		public override void Execute ()
		{
			BibaGameModel.SelectedEquipments.Clear ();
		}
	}
}