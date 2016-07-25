using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class TagScanCompletedCommand : Command
	{
		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		public override void Execute ()
		{
			if (BibaDeviceSession.SelectedEquipments.Count > 0)
			{
				BibaDeviceSession.TagScanned = true;
			}
		}
	}
}