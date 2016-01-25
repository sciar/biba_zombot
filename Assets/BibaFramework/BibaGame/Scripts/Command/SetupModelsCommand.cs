using strange.extensions.command.impl;
using System;
using BibaFramework.BibaMenu;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class SetupGameModelCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

		[Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
			SetupGameModel();
			SetupSessionModel();
        }

		void SetupGameModel()
		{
			SetMenuStateConditionSignal.Dispatch(MenuStateCondition.PrivacyEnabled, BibaGameModel.PrivacyEnabled);
			SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HowToEnabled, BibaGameModel.HowToEnabled);
			SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HelpBubblesEnabled, BibaGameModel.HelpBubblesEnabled);
		}

		void SetupSessionModel()
		{
			BibaSessionModel.SessionInfo = new SessionInfo();
			BibaSessionModel.SessionInfo.UUID = SystemInfo.deviceUniqueIdentifier;
            BibaSessionModel.SessionInfo.SessionID = Guid.NewGuid().ToString();
			BibaSessionModel.SessionInfo.DeviceModel = SystemInfo.deviceModel;
			BibaSessionModel.SessionInfo.DeviceOS = SystemInfo.operatingSystem;
		}
    }
}