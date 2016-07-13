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
		public BibaSystem BibaSystem { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
			BibaSystem.LastChartBoostTime = DateTime.UtcNow;
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowChartBoost, false);

			DataService.Save ();
        }
    }
}