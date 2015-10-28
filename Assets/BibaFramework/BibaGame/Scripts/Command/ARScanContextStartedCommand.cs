using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;
using System;

namespace BibaFramework.BibaGame
{
    public class ARScanContextStartedCommand : Command
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
                if((DateTime.UtcNow - BibaGameModel.LastCameraReminderTime) < new TimeSpan(0,0,BibaGameConstants.ONE_DAY_IN_SECONDS))
                {
                    shouldSkipTag = true;
                }
            }

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.ShowTagScan, !shouldSkipTag);
        }
    }
}