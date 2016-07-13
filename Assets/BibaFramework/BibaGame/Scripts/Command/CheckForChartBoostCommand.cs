using strange.extensions.command.impl;
using BibaFramework.BibaMenu;
using System;

namespace BibaFramework.BibaGame
{
    public class CheckForChartBoostCommand : Command
    {
        [Inject]
		public BibaSystem BibaSystem { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			SetMenuStateConditionSignal.Dispatch (MenuStateCondition.ShowChartBoost, (DateTime.UtcNow - BibaSystem.LastChartBoostTime) >= BibaGameConstants.CHARTBOOST_CHECK_DURATION);
        }
    }
}