using strange.extensions.command.impl;
using System;
using BibaFramework.BibaMenu;
using UnityEngine;
using System.Collections.Generic;

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

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
			SetupGameModel();
			SetupSessionModel();
            CheckForGameModelMigration();
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

        //Where we handle the migration of BibaGameModel
        void CheckForGameModelMigration()
        {
            if (BibaGameModel.FrameworkVersion < BibaGameConstants.FRAMEWORK_VERSION)
            {
                //Reset Achievements since we changed the way AchievementId is stored
                if(BibaGameModel.FrameworkVersion == 0)
                {
                    BibaGameModel.CompletedAchievements = new List<BibaAchievement>();
                }

                BibaGameModel.FrameworkVersion = BibaGameConstants.FRAMEWORK_VERSION;
                DataService.WriteGameModel();
            }
        }
    }
}