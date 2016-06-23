using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using System;

namespace BibaFramework.BibaGame
{
    public class CheckForInactiveResetCommand : Command
    {
        [Inject]
		public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			var timeInactive = DateTime.UtcNow - BibaGameModel.LastPlayedTime;
			if (timeInactive >= BibaGameConstants.INACTIVE_DURATION && BibaGameModel.SelectedEquipments.Count > 0)
            {
                SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowInactive, true);
            }
        }
    }
}