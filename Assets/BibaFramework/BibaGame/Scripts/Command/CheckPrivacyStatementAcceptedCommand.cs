using strange.extensions.command.impl;
using System;
using BibaFramework.BibaData;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class CheckPrivacyStatementAcceptedCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.PrivacyAgreementAccepted, BibaGameModel.PrivacyPolicyAccepted);
        }
    }
}