using strange.extensions.command.impl;
using System;
using BibaFramework.BibaMenu;
using UnityEngine;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class SetupSystemModelCommand : Command
    {
        [Inject]
		public BibaSystem BibaSystem { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
			ResetMenuStateCondition();
			SetupSystemModel();
			SetupMenuStateByGameModel();
            CheckForGameModelMigration();

			SetFramerate ();
        }

		void SetupSystemModel()
		{
			BibaSystem.UUID = Guid.NewGuid().ToString();
			BibaSystem.DeviceModel = SystemInfo.deviceModel;
			BibaSystem.DeviceOS = SystemInfo.operatingSystem;
		}

		void ResetMenuStateCondition()
		{
			SetMenuStateConditionSignal.Dispatch (MenuStateCondition.ShowInactive, false);
			SetMenuStateConditionSignal.Dispatch (MenuStateCondition.ShowChartBoost, false);
			SetMenuStateConditionSignal.Dispatch (MenuStateCondition.ShowBibaPresent, false);
			SetMenuStateConditionSignal.Dispatch (MenuStateCondition.ShowTagScan, false);
		}

		void SetupMenuStateByGameModel()
		{
			SetMenuStateConditionSignal.Dispatch(MenuStateCondition.PrivacyEnabled, BibaSystem.PrivacyEnabled);
			SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HowToEnabled, BibaSystem.HowToEnabled);
			SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HelpBubblesEnabled, BibaSystem.HelpBubblesEnabled);
		}
			
        //Where we handle the migration of BibaGameModel
        void CheckForGameModelMigration()
        {
			if (BibaSystem.FrameworkVersion < BibaGameConstants.FRAMEWORK_VERSION)
            {
                //Reset Achievements since we changed the way AchievementId is stored
				if(BibaSystem.FrameworkVersion == 0)
                {
					BibaSystem.CompletedAchievements = new List<BibaAchievement>();
                }

				BibaSystem.FrameworkVersion = BibaGameConstants.FRAMEWORK_VERSION;
				DataService.Save();
            }
        }

		void SetFramerate()
		{
			Application.targetFrameRate = 60;
			QualitySettings.vSyncCount = 0;
		}
	}
}