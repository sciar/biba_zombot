using strange.extensions.command.impl;
using System;
using BibaFramework.BibaData;
using BibaFramework.BibaMenu;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class CheckForInactiveResetCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
            var timeInactive = DateTime.UtcNow - BibaGameModel.LastPlayedTime;
            if (timeInactive >= TimeSpan.FromSeconds(BibaGameConstants.INACTIVE_SECONDS))
            {
                SetMenuStateConditionSignal.Dispatch(MenuStateCondition.Inactive, true);
                SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Reset);
            }
        }
    }
}