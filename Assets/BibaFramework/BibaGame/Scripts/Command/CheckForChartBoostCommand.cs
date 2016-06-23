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
			SetMenuStateConditionSignal.Dispatch (MenuStateCondition.ShowChartBoost, (DateTime.UtcNow - BibaGameModel.LastChartBoostTime) >= BibaGameConstants.CHARTBOOST_CHECK_DURATION);
        }
    }
}