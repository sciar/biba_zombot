using strange.extensions.command.impl;
using BibaFramework.BibaMenu;
using System;
using System.Linq;

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
            var unFinishedAchievementConfig = BibaGameConfig.AchievementConfigs.Where(config => BibaGameModel.CompletedAchievements.FindIndex(completedAchievement => completedAchievement.Id == config.Id) == -1);

            //Check settings all unFinished achievements
            foreach (var config in unFinishedAchievementConfig)
            {
                var equipment = BibaGameModel.TotalPlayedEquipments.Find(equip => equip.EquipmentType == config.EquipmentType);
                if(equipment.TimePlayed >= config.TimePlayed)
                {
                    //New achievement obtained
                    var newAchievement = new BibaAchievement(config);
                    BibaGameModel.CompletedAchievements.Add(newAchievement);
                    DataService.WriteGameModel();
                }
            }
        }
    }
}