using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaNetwork
{
	public enum Environment
	{
		Development,
		Production
	}

    public class BibaContentConstants
    {
        //Overwrite for different games
        public const string CI_GAME_ID = "biba-framework";

		//Change for Production or Development build
		public const Environment ENVIRONMENT = Environment.Production;

		private const string DEV_PREFIX = "dev-";
		public static string S3BucketName { 
			get { 
				return ENVIRONMENT == Environment.Development ? DEV_PREFIX + CI_GAME_ID : CI_GAME_ID;
			} 
		}

        public const string AWS_IDENTITY_POOL_ID = "us-east-1:be839779-a7ee-4f18-9c5a-e1651f030bea";

		private static string PLATFORM {
			get {
				return Application.platform == RuntimePlatform.Android ? "Android" : "iOS";
			}
		}

        public const string SPECIAL_SCENE_SETTINGS_FILE = "settings_specialscene" + TEXT_EXTENSION;
        public const string LOCALIZATION_SETTINGS_FILE = "settings_localization" + TEXT_EXTENSION;
        public const string ACHIEVEMENT_SETTINGS_FILE = "settings_achievement" + TEXT_EXTENSION;
        public const string MANIFEST_FILENAME = "manifest" + TEXT_EXTENSION;
		public const string BIBAVERSION_FILE = "bibaversion" + TEXT_EXTENSION;
        public const string TEXT_EXTENSION = ".txt";
        public const string UNITY3D_EXTENSION = ".unity3d";
        
        public static string GetRelativePath(string fileName)
        {
			return PLATFORM + "/" + fileName;
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