using strange.extensions.command.impl;
using BibaFramework.BibaMenu;
using System;

namespace BibaFramework.BibaGame
{
    public class CheckForChartBoostCommand : Command
    {
        [Inject]
		public BibaDevice BibaDevice { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			SetMenuStateConditionSignal.Dispatch (MenuStateCondition.ShowChartBoost, (DateTime.UtcNow - BibaDevice.LastChartBoostTime) >= BibaGameConstants.CHARTBOOST_CHECK_DURATION);
        }
    }
}