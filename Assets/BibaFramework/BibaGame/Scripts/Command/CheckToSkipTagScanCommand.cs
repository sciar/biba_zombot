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
		public BibaSession BibaSession { get; set; }

		[Inject]
		public BibaSystem BibaSystem { get; set; }

        public override void Execute ()
        {
            var shouldSkipTag = false;

			if(!BibaSession.TagEnabled)
            {
                shouldSkipTag = true;
            }
            else
            {
                //If the CameraDisabled reminder is shown less than a day ago 
				var timeSinceLastCameraReminder = DateTime.UtcNow - BibaSystem.LastCameraReminderTime;
                shouldSkipTag = timeSinceLastCameraReminder < BibaGameConstants.AR_REMINDER_DURATION;
            }

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowTagScan, !shouldSkipTag);
        }
    }
}