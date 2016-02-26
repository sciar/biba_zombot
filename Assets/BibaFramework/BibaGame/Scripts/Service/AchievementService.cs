using System.Collections.Generic;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public class AchievementService
    {
        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        private BibaAchievementSettings _settings;
        private BibaAchievementSettings Settings {
            get 
            {
                if(_settings == null)
                {
                    _settings = DataService.ReadFromDisk<BibaAchievementSettings>(BibaDataConstants.ACHIEVEMENT_SETTINGS_PATH);

                    foreach (var setting in _settings.AchievementSettings)
                    {
                        var index = BibaGameModel.CompletedAchievements.FindIndex(achievement => achievement.Id == setting.Id);
                        if(index != -1)
                        {
                            BibaGameModel.CompletedAchievements[index].Config = setting;
                        }
                    }
                }
                return _settings;
            }
        }

        private IEnumerable<BibaAchievementConfig> InCompletedAchievements {
            get 
            {
                return Settings.AchievementSettings.Where(config => BibaGameModel.CompletedAchievements.FindIndex(completedAchievement => completedAchievement.Id == config.Id) == -1);
            }
        }

        public void CheckAndCompleteAchievements()
        {
            //Check settings all unFinished achievements
            foreach (var config in InCompletedAchievements)
            {
                var equipment = BibaGameModel.TotalPlayedEquipments.Find(equip => equip.EquipmentType == config.EquipmentType);
                if(IsBasicAchievementCompleted(equipment, config))
                {
                    //New achievement obtained
                    var newAchievement = new BibaAchievement(config);
                    BibaGameModel.CompletedAchievements.Add(newAchievement);
                    DataService.WriteGameModel();
                }
            }
        }

        bool IsBasicAchievementCompleted(BibaEquipment equipment, BibaAchievementConfig config)
        {
            return equipment.NumberOfTimePlayed >= config.TimePlayed && !(config is BibaSeasonalAchievementConfig);
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