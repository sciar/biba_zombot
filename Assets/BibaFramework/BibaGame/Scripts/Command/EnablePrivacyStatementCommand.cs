using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class EnablePrivacyStatementCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
            BibaGameModel.PrivacyPolicyAccepted = true;
            DataService.WriteGameModel();

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.PrivacyAgreementAccepted, true);
        }
    }
}