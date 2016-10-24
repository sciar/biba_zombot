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
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public BibaDevice BibaDevice { get; set; }

        public override void Execute ()
        {
            var shouldSkipTag = false;

			if(!BibaDeviceSession.TagEnabled)
            {
                shouldSkipTag = true;
            }
            else
            {
                //If the CameraDisabled reminder is shown less than a day ago 
				var timeSinceLastCameraReminder = DateTime.UtcNow - BibaDevice.LastCameraReminderTime;
                shouldSkipTag = timeSinceLastCameraReminder < BibaGameConstants.AR_REMINDER_DURATION;
            }

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowTagScan, !shouldSkipTag);
        }
    }
}