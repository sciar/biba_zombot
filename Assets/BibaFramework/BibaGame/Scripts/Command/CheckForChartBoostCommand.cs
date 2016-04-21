using strange.extensions.command.impl;
using BibaFramework.BibaMenu;
using System;

namespace BibaFramework.BibaGame
{
    public class CheckForChartBoostCommand : Command
    {
        [Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			if (BibaSessionModel.LastChartBoostTime == DateTime.MaxValue)
            {
				BibaSessionModel.LastChartBoostTime = DateTime.UtcNow;
            }

			var timeInactive = DateTime.UtcNow - BibaSessionModel.LastChartBoostTime;
            if (timeInactive >= BibaGameConstants.CHARTBOOST_CHECK_DURATION)
            {
                SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowChartBoost, true);
            }
        }
    }
}