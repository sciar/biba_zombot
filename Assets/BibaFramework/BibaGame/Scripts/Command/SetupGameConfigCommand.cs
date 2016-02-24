using strange.extensions.command.impl;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public class SetupGameConfigCommand : Command
    {
        [Inject]
        public BibaGameConfig BibaGameConfig { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService LoaderService { get; set; }

        [Inject]
        public ICDNService CDNService { get; set; }

        public override void Execute ()
        {
            CDNService.UpdateFromCDN();

            SetFramerate();
            LoadAchievementSettings();
        }

        void SetFramerate()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }

        void LoadAchievementSettings()
        {
            var configs = LoaderService.ReadFromDisk<BibaAchievementSettings>(BibaDataConstants.ACHIEVEMENT_SETTINGS_PATH);
            BibaGameConfig.BibaAchievementSettings = configs;

            foreach (var config in BibaGameConfig.BibaAchievementSettings.AchievementSettings)
            {
                var index = BibaGameModel.CompletedAchievements.FindIndex(achievement => achievement.Id == config.Id);
                if(index != -1)
                {
                    BibaGameModel.CompletedAchievements[index].Config = config;
                }
            }
        }
    }
}