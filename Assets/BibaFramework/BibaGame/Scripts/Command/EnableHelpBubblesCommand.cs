using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class EnableHelpBubblesCommand : Command
    { 
        [Inject]
        public bool Status { get; set; }

        [Inject]
		public BibaDevice BibaDevice { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			BibaDevice.HelpBubblesEnabled = Status;
			DataService.Save();

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HelpBubblesEnabled, Status);
        }
    }
}