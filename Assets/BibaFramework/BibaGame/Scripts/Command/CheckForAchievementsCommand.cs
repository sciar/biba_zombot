using strange.extensions.command.impl;
using BibaFramework.BibaMenu;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class CheckForAchievementsCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public BibaGameConfig BibaGameConfig { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
            var unFinishedAchievementsConfig = AchievementsToCheck;

            //Check settings all unFinished achievements
            foreach (var config in unFinishedAchievementsConfig)
            {
                var equipment = BibaGameModel.TotalPlayedEquipments.Find(equip => equip.EquipmentType == config.EquipmentType);
                if(IsAchievementCompleted(equipment, config))
                {
                    //New achievement obtained
                    var newAchievement = new BibaAchievement(config);
                    BibaGameModel.CompletedAchievements.Add(newAchievement);
                    DataService.WriteGameModel();
                }
            }
        }

        protected IEnumerable<BibaAchievementConfig> AchievementsToCheck {
            get {
                return BibaGameConfig.BibaAchievementSettings.AchievementSettings.Where(config => BibaGameModel.CompletedAchievements.FindIndex(completedAchievement => completedAchievement.Id == config.Id) == -1);
            }
        }

        protected virtual bool IsAchievementCompleted(BibaEquipment equipment, BibaAchievementConfig config)
        {
            return equipment.NumberOfTimePlayed >= config.TimePlayed && !(config is BibaSeasonalAchievementConfig);
        }
    }
}