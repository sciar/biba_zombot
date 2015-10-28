using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using System;

namespace BibaFramework.BibaGame
{
    public class LogChartBoostDisplayTimeCommand : Command
    {
        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
            BibaGameModel.LastChartBoostTime = DateTime.UtcNow;
            DataService.WriteGameModel();

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowChartBoost, false);
        }
    }
}