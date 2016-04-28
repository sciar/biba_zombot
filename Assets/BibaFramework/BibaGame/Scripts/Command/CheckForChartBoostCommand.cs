using strange.extensions.command.impl;
using BibaFramework.BibaMenu;
using System;

namespace BibaFramework.BibaGame
{
    public class CheckForChartBoostCommand : Command
    {
        [Inject]
		public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			if (BibaGameModel.LastChartBoostTime == DateTime.MaxValue)
            {
				BibaGameModel.LastChartBoostTime = DateTime.UtcNow;
            }

			var timeInactive = DateTime.UtcNow - BibaGameModel.LastChartBoostTime;
            if (timeInactive >= BibaGameConstants.CHARTBOOST_CHECK_DURATION)
            {
                SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowChartBoost, true);
            }
        }
    }
}