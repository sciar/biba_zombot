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
		public BibaSessionModel BibaSessionModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
			BibaSessionModel.LastChartBoostTime = DateTime.UtcNow;
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowChartBoost, false);
        }
    }
}