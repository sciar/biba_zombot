using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaDataConstants
    {
        //GameModel
        public const string GAME_MODEL_DATA_PATH = "gamemodel.txt";

        //GameSettings
        public static readonly string SPECIAL_SCENE_SETTINGS_PATH = Application.dataPath + "/Resources/settings_specialscene.txt";
        public static readonly string LOCALIZATION_SETTINGS_PATH = Application.dataPath + "/Resources/settings_localization.txt";
        public static readonly string ACHIEVEMENT_SETTINGS_PATH = Application.dataPath + "/Resources/settings_achievement.txt";
    }
}