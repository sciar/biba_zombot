using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using System;

namespace BibaFramework.BibaGame
{
    public class InactiveContextStartCommand : Command
    {
        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowInactive, false);
        }
    }
}