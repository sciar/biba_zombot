using strange.extensions.command.impl;
using BibaFramework.BibaGame;
using UnityEngine;

namespace BibaFramework.BibaNetwork
{
    public class ContentUpdatedFromCDNCommand : Command
    {
        [Inject]
        public string ContentUpdated { get; set; }

        [Inject]
        public AchievementService AchievementService { get; set; } 

        [Inject]
        public LocalizationService LocalizationService { get; set; } 

        [Inject]
        public SpecialSceneService SpecialSceneService { get; set; }

        public override void Execute ()
        {
			if (ContentUpdated == BibaContentConstants.GetRelativePath(BibaContentConstants.ACHIEVEMENT_SETTINGS_FILE))
            {
                AchievementService.ReloadContent();
            }

			if (ContentUpdated == BibaContentConstants.GetRelativePath(BibaContentConstants.LOCALIZATION_SETTINGS_FILE))
            {
                LocalizationService.ReloadContent();
            }

			if (ContentUpdated == BibaContentConstants.GetRelativePath(BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE))
            {
                SpecialSceneService.ReloadContent();
            }
        }
    }
}