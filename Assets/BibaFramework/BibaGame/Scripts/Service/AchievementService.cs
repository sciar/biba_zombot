using System.Collections.Generic;
using System.Linq;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public class AchievementService : BaseSettingsService<BibaAchievementSettings>
    {
        [Inject]
		public BibaSystem BibaSystem { get; set; }

		[Inject]
		public LocalizationService LocalizationService { get; set; }

        public override string SettingsFileName {
            get {
                return BibaContentConstants.ACHIEVEMENT_SETTINGS_FILE;
            }
        }

        #region - IContentUpdated
        public override void ReloadContent()
        {
            _settings = DataService.ReadFromDisk<BibaAchievementSettings>(ContentFilePath);
            UpdateGameModel();
        }
        #endregion

		public string GetAchievementText(string achievementId)
		{
			var setting = _settings.AchievementSettings.Find(achi => achi.Id == achievementId);
			return setting != null ? LocalizationService.GetText (setting.DescriptionPrefix) + " " + LocalizationService.GetText (setting.DescriptionSuffix) : string.Empty;
		}

        void UpdateGameModel()
        {
            foreach (var setting in _settings.AchievementSettings)
            {
				var index = BibaSystem.CompletedAchievements.FindIndex(achievement => achievement.Id == setting.Id);
                if(index != -1)
                {
					BibaSystem.CompletedAchievements[index].Config = setting;
                }
            }
        }

        private IEnumerable<BibaAchievementConfig> InCompletedAchievements {
            get 
            {
				return Settings.AchievementSettings.Where(config => BibaSystem.CompletedAchievements.FindIndex(completedAchievement => completedAchievement.Id == config.Id) == -1);
            }
        }

        public void CheckAndCompleteAchievements()
        {
            //Check settings all unFinished achievements
            foreach (var config in InCompletedAchievements)
            {
				var equipment = BibaSystem.PlayedEquipments.Find(equip => equip.EquipmentType == config.EquipmentType);

                if((config is BibaAchievementConfig && IsBasicAchievementCompleted(equipment, config)) ||
                   (config is BibaSeasonalAchievementConfig && IsSeasonalAchievementCompleted(equipment, config)))
                {
                    //New achievement obtained
                    var newAchievement = new BibaAchievement(config);
					BibaSystem.CompletedAchievements.Add(newAchievement);
					DataService.Save();
                }
            }
        }

        bool IsBasicAchievementCompleted(BibaEquipment equipment, BibaAchievementConfig config)
        {
            return equipment.NumberOfTimePlayed >= config.TimePlayed;
        }

        bool IsSeasonalAchievementCompleted(BibaEquipment equipment, BibaAchievementConfig config)
        {
            if (config is BibaSeasonalAchievementConfig)
            {
                var seasonalConfig = (BibaSeasonalAchievementConfig)config;
                var timePlayedInSeason = equipment.TimesPlayed.Where(date => date >= seasonalConfig.StartDate && date <= seasonalConfig.EndDate).Count();
                return timePlayedInSeason >= seasonalConfig.TimePlayed;
            }
            return false;
        }
    }
}