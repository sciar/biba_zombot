using strange.extensions.command.impl;
using System;
using BibaFramework.BibaMenu;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class SetupModelsCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public BibaSessionModel BibaSessionModel { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
            SetupByGameModel();
            SetupSessionModel();
        }

        void SetupByGameModel()
        {
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.PrivacyEnabled, BibaGameModel.PrivacyEnabled);
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HowToEnabled, BibaGameModel.HowToEnabled);
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HelpBubblesEnabled, BibaGameModel.HelpBubblesEnabled);
        }

        void SetupSessionModel()
        {
            BibaSessionModel.UDID = SystemInfo.deviceUniqueIdentifier;
            BibaSessionModel.DeviceOS = SystemInfo.operatingSystem;
            BibaSessionModel.DeviceModel = SystemInfo.deviceModel;
        }
    }
}