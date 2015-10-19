using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using System;

namespace BibaFramework.BibaGame
{
    public class ChartBoostContextStartCommand : Command
    {
        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
            BibaGameModel.LastChartBoostDisplayedTime = DateTime.UtcNow;
            DataService.WriteGameModel();

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowChartBoost, false);
        }
    }
}