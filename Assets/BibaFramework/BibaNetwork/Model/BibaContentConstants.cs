using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaNetwork
{
    public class BibaContentConstants
    {
        //Overwrite for different games
        public const string CI_GAME_ID = "biba-framework";

        public const string AWS_IDENTITY_POOL_ID = "us-east-1:be839779-a7ee-4f18-9c5a-e1651f030bea";

        #if UNITY_IPHONE
        private static readonly string PLATFORM_FOLDER = "iOS";
        #endif

        #if UNITY_ANDROID
        private static readonly string PLATFORM_FOLDER = "Android";
        #endif

        public const string SPECIAL_SCENE_SETTINGS_FILE = "settings_specialscene" + TEXT_EXTENSION;
        public const string LOCALIZATION_SETTINGS_FILE = "settings_localization" + TEXT_EXTENSION;
        public const string ACHIEVEMENT_SETTINGS_FILE = "settings_achievement" + TEXT_EXTENSION;
        public const string MANIFEST_FILENAME = "manifest" + TEXT_EXTENSION;
        public const string TEXT_EXTENSION = ".txt";
        public const string UNITY3D_EXTENSION = ".unity3d";
        
        public static string GetRelativePath(string fileName)
        {
            return PLATFORM_FOLDER + "/" + fileName;
        }

        public static string GetResourceFilePath(string fileName)
        {
            return Application.dataPath + "/Resources/" + GetRelativePath(fileName);
        }

        public static string GetPersistedPath(string fileName)
        {
            return Application.persistentDataPath + "/" + GetRelativePath(fileName);
        }
    }
}