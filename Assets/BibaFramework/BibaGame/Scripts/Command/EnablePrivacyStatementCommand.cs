using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class EnablePrivacyStatementCommand : Command
    {
        [Inject]
		public BibaSystem BibaSystem { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			BibaSystem.PrivacyEnabled = true;
			DataService.Save();

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.PrivacyEnabled, true);
        }
    }
}