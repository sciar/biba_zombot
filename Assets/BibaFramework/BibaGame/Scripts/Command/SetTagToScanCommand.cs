using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class SetTagToScanCommand : Command
    {
        [Inject]
        public SetTagToScanAtViewSignal SetTagToScanAtViewSignal { get; set; }

        [Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

        public override void Execute ()
        {
			if (BibaDeviceSession.SelectedEquipments.Count > 0)
            {
				var rndIndex = Random.Range(0, BibaDeviceSession.SelectedEquipments.Count);
				SetTagToScanAtViewSignal.Dispatch(BibaDeviceSession.SelectedEquipments[rndIndex]);
            }
        }
    }
}