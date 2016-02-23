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

        public static readonly string MANIFEST_FILENAME = "manifest.txt";

        public static string GetContentRelativePath(string fileName)
        {
            return PLATFORM_FOLDER + "/" + fileName;
        }

        public static string GetResourceContentFilePath(string fileName)
        {
            return Application.dataPath + "/Resources/" + GetContentRelativePath(fileName);
        }

        public static string GetOnDiskContentFilePath(string fileName)
        {
            return Application.persistentDataPath + "/" + GetContentRelativePath(fileName);
        }
    }
}