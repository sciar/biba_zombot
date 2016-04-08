using UnityEngine;
using UnityEditor;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaEditor
{
    public class BibaEditorConstants
    {
        public static readonly string SCENE_ASSETBUNDLES_INPUT_PATH = Application.dataPath + "/BibaContent/Input";
        public static readonly string OPTIONAL_ASSETBUNDLES_FOLDER = SCENE_ASSETBUNDLES_INPUT_PATH + "/Optional/";

        public static string GetContentOutputPath(string fileName)
        {
            return  Application.dataPath + "/BibaContent/Output/" + GetRelativePath(fileName);
        }

		public static string GetResourceFilePath(string fileName)
		{
			return Application.dataPath + "/Resources/" + GetRelativePath(fileName);
		}

        public const string UNITY_EXTENSION = ".unity";
        public const string S3UPLOADER_PATH = "S3Uploader/S3Uploader.exe";
        public const string MONO_PATH = "/Library/Frameworks/Mono.framework/Versions/Current/bin/mono";

        //Google Doc Settings
        public const string REGEX_STARTDATE_ENDDATE = "(?<startDate>(0[1-9]|1[0-2])[/](0[1-9]|[1-2][0-9]|3[0-1]))[ ]*-[ ]*(?<endDate>(0[1-9]|1[0-2])[/](0[1-9]|[1-2][0-9]|3[0-1]))";
        public const string REGEX_GROUP_STARTDATE = "startDate";
        public const string REGEX_GROUP_ENDDATE = "endDate";
        public const string REGEX_TIME_PLAYED = "played([1-9][0-9]*)";
        public const string DATETIME_PARSE_EXACT_FORMAT = "MM/dd";

		private static string GetRelativePath(string fileName)
		{
			var platform = EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android ? "Android" : "iOS";
			return platform + "/" + fileName;
		}
    }
}