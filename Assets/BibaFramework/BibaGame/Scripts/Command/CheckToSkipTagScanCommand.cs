using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;
using System;

namespace BibaFramework.BibaGame
{
    public class CheckToSkipTagScanCommand : Command
    {
        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        [Inject]
		public BibaGameModel BibaGameModel { get; set; }

        public override void Execute ()
        {
            var shouldSkipTag = false;

			if(!BibaGameModel.TagEnabled)
            {
                shouldSkipTag = true;
            }
            else
            {
                //If the CameraDisabled reminder is shown less than a day ago 
				var timeSinceLastCameraReminder = DateTime.UtcNow - BibaGameModel.LastCameraReminderTime;
                shouldSkipTag = timeSinceLastCameraReminder < BibaGameConstants.AR_REMINDER_DURATION;
            }

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowTagScan, !shouldSkipTag);
        }
    }
}