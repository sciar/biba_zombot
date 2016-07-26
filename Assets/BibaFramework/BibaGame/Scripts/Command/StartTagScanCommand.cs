using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class StartTagScanCommand : Command
    {
        [Inject]
        public SetTagToScanAtViewSignal SetTagToScanAtViewSignal { get; set; }

        [Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public ToggleTagScanSignal ToggleTagScanSignal { get; set; }

        public override void Execute ()
        {
			if (BibaDeviceSession.SelectedEquipments.Count > 0)
            {
				ToggleTagScanSignal.Dispatch (true);

				var rndIndex = Random.Range(0, BibaDeviceSession.SelectedEquipments.Count);
				BibaDeviceSession.TagToScan = BibaDeviceSession.SelectedEquipments [rndIndex].TagType;

				SetTagToScanAtViewSignal.Dispatch();
            }
        }
    }
}