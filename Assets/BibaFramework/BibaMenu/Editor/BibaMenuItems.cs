using BibaFramework.BibaMenu;
using BibaFramework.Utility;
using BibaFramework.BibaGame;
using UnityEditor;

namespace BibaFramework.BibaMenuEditor
{
    public class BibaMenuItems
	{
        [MenuItem("Biba/Create Achievement Settings")]
        public static void CreateAchievementAsset ()
        {
            ScriptableObjectUtility.CreateAsset<BibaAchievementConfig> ();
        }

        [MenuItem("Biba/Create Seasonal Achievement Settings")]
        public static void CreateSeasonalAchievementAsset ()
        {
            ScriptableObjectUtility.CreateAsset<BibaSeasonalAchievementConfig> ();
        }
	}
}
