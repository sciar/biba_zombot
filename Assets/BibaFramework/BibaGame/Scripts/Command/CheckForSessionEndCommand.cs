using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using System;

namespace BibaFramework.BibaGame
{
    public class CheckForSessionEndCommand : Command
    {
        [Inject]
		public BibaSystem BibaSystem { get; set; }

		[Inject]
		public BibaSession BibaSession { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			var timeInactive = DateTime.UtcNow - BibaSystem.LastPlayedTime;
			if (timeInactive >= BibaGameConstants.INACTIVE_DURATION && BibaSession.SelectedEquipments.Count > 0)
            {
                SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowInactive, true);
            }
        }
    }
}