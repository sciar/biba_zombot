using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class EnablePrivacyStatementCommand : Command
    {
        [Inject]
		public BibaDevice BibaDevice { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			BibaDevice.PrivacyEnabled = true;
			DataService.Save();

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.PrivacyEnabled, true);
        }
    }
}