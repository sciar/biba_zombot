using strange.extensions.command.impl;
using System;
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

		[Inject]
		public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
            var timeInactive = DateTime.UtcNow - BibaGameModel.LastPlayedTime;
            if (timeInactive >= TimeSpan.FromSeconds(BibaGameConstants.ONE_HOUR_IN_SECONDS) && BibaGameModel.SelectedEquipments.Count > 0)
            {
                SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowInactive, true);

				if(BibaSceneStack.Count > 0 && !BibaSceneStack.Peek() is IntroMenuState)
				{
					SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Reset);
				} 
            }
        }
    }
}