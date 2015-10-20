using strange.extensions.command.impl;
using UnityEngine;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public class SetupGameConfigCommand : Command
    {
        [Inject]
        public BibaGameConfig BibaGameConfig { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        public override void Execute ()
        {
            SetupAchievementConfig();
        }

        void SetupAchievementConfig()
        {
            var configs = Resources.LoadAll<BibaAchievementConfig>(BibaDataConstants.RESOURCE_FOLDER_ACHIEVEMENT_CONFIG_DATA_PATH);
            BibaGameConfig.AchievementConfigs = configs.ToList();

            foreach (var config in BibaGameConfig.AchievementConfigs)
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