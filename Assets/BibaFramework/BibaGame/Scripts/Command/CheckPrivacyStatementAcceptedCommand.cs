using strange.extensions.command.impl;
using System;
using BibaFramework.BibaData;
using BibaFramework.BibaMenu;
using UnityEngine;

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
            Debug.Log(BibaGameModel.PrivacyPolicyAccepted.ToString());
            Debug.Log(BibaGameModel.Equipments.Count.ToString());
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.PrivacyAgreementAccepted, BibaGameModel.PrivacyPolicyAccepted);
        }
    }
}