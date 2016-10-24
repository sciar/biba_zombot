using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using System;

namespace BibaFramework.BibaGame
{
    public class CheckForSessionEndCommand : Command
    {
        [Inject]
		public BibaDevice BibaDevice { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			var timeInactive = DateTime.UtcNow - BibaDevice.LastPlayedTime;
			if (timeInactive >= BibaGameConstants.INACTIVE_DURATION && BibaDeviceSession.SelectedEquipments.Count > 0)
            {
                SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowInactive, true);
            }
        }
    }
}