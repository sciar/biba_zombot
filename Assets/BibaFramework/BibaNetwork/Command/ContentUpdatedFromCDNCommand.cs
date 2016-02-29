using strange.extensions.command.impl;
using BibaFramework.BibaGame;

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
            if (ContentUpdated == BibaContentConstants.ACHIEVEMENT_SETTINGS_FILE)
            {
                AchievementService.ReloadSettings();
            }

            if (ContentUpdated == BibaContentConstants.LOCALIZATION_SETTINGS_FILE)
            {
                LocalizationService.ReloadSettings();
            }

            if (ContentUpdated == BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE)
            {
                SpecialSceneService.ReloadSettings();
            }
        }
    }
}