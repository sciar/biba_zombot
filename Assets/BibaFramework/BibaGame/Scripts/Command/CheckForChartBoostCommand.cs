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
            var timeInactive = DateTime.UtcNow - BibaGameModel.LastChartBoostDisplayedTime;
            if (timeInactive >= TimeSpan.FromSeconds(BibaGameConstants.CHARTBOOST_INTERVAL_SECONDS))
            {
                SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowChartBoost, true);
            }
        }
    }
}