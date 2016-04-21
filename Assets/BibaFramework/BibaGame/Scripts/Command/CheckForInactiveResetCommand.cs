using strange.extensions.command.impl;
using System;
using BibaFramework.BibaMenu;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class CheckForInactiveResetCommand : Command
    {
        [Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

		[Inject]
		public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
			var timeInactive = DateTime.UtcNow - BibaSessionModel.LastPlayedTime;
			if (timeInactive >= BibaGameConstants.INACTIVE_DURATION && BibaSessionModel.SelectedEquipments.Count > 0)
            {
                SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowInactive, true);

				if(BibaSceneStack.Count > 0 && BibaSceneStack.Peek().GetType() != typeof(IntroMenuState))
				{
					SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Reset);
				} 
            }
        }
    }
}