using System;
using System.Collections.Generic;
using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class SetDeviceModelCommand : Command
    {
        [Inject]
		public BibaDevice BibaDevice { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
			ResetMenuStateCondition();
			SetupMenuStateByGameModel();
            CheckForGameModelMigration();

			SetFramerate ();
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
			SetMenuStateConditionSignal.Dispatch(MenuStateCondition.PrivacyEnabled, BibaDevice.PrivacyEnabled);
			SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HowToEnabled, BibaDevice.HowToEnabled);
			SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HelpBubblesEnabled, BibaDevice.HelpBubblesEnabled);
		}
			
        //Where we handle the migration of BibaGameModel
        void CheckForGameModelMigration()
        {
			if (BibaDevice.FrameworkVersion < BibaGameConstants.FRAMEWORK_VERSION)
            {
                //Reset Achievements since we changed the way AchievementId is stored
				if(BibaDevice.FrameworkVersion == 0)
                {
					BibaDevice.CompletedAchievements = new List<BibaAchievement>();
                }

				BibaDevice.FrameworkVersion = BibaGameConstants.FRAMEWORK_VERSION;
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