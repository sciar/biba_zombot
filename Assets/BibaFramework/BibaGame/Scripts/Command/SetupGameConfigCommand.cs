using strange.extensions.command.impl;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

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
            SetFramerate();
            SetAchievementConfig();
            SetLocalizationDictionary();
        }

        void SetFramerate()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }

        void SetAchievementConfig()
        {
            var configs = Resources.LoadAll<BibaAchievementConfig>(BibaDataConstants.RESOURCE_ACHIEVEMENT_CONFIG_FOLDER_PATH);
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

        void SetLocalizationDictionary()
        {
            var localizationSettings = Resources.Load<BibaLocalizationSettings>(BibaDataConstants.RESOURCE_LOCALIZATION_FILE_PATH);
            var localizationDict = new Dictionary<string, Dictionary<SystemLanguage, string>>();

            foreach (var localization in localizationSettings.Localizations)
            {
                if(!localizationDict.ContainsKey(localization.Key))
                {
                    localizationDict.Add(localization.Key, new Dictionary<SystemLanguage, string>());
                }

                var localizationKeyDictionary = localizationDict[localization.Key];
                if(!localizationKeyDictionary.ContainsKey(localization.Language))
                {
                    localizationKeyDictionary.Add(localization.Language, localization.Text);
                }
                else
                {
                    localizationKeyDictionary[localization.Language] = localization.Text;
                }
            }

            injectionBinder.Bind<Dictionary<string, Dictionary<SystemLanguage, string>>>().To(localizationDict).ToName(BibaDataConstants.LOCALIZATION_FILE).ToSingleton().CrossContext();
        }
    }
}