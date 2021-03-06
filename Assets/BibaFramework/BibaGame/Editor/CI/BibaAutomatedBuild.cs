using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using BibaFramework.BibaGame;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaEditor
{
	public static class BibaAutomatedBuild
	{
		private const string IOS_OUTPUT_PATH = "Build/iOS";

		private const string ANDROID_OUTPUT_PATH = "Build/Android";
		private const string ANDROID_APK = "android.apk";
        private const string KEYSTORE_PASSWORD = "#sling2Rock!";

		private const string ANDROID_SDK_FOLDER_KEY = "AndroidSdkRoot";
		private const string ANDROID_SDK_FOLDER_PATH = "/Users/Jenkins/Library/Android/sdk";

		[MenuItem("Biba/Build Jobs/Build iOS")]
		public static void BuildIOS ()
		{
            // Set Build Target
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);

            // Update Settings and Manifest
            BibaContentUploader.RunContentPipeLineWithoutUpload();

            // Set Build Number
			PlayerSettings.iOS.buildNumber = JenkinsBuildNumber.ToString();
			StoreBuildNumber ();
          
            // Get filename
            var path = IOS_OUTPUT_PATH;
            BuildPlayer(path, BuildTarget.iOS);
		}

        [MenuItem("Biba/Build Jobs/Build Android")]
		public static void BuildAndroid ()
		{
            // Set Build Target
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);

            // Update Settings and Manifest
            BibaContentUploader.RunContentPipeLineWithoutUpload();
            
            // Set Build Number
			PlayerSettings.Android.bundleVersionCode = JenkinsBuildNumber;
			StoreBuildNumber ();

			// Set KeyStore
            PlayerSettings.Android.keystorePass = KEYSTORE_PASSWORD;
            PlayerSettings.Android.keyaliasName = PlayerSettings.Android.keyaliasName; 
            PlayerSettings.Android.keyaliasPass = KEYSTORE_PASSWORD;

			// Set SDK path
			EditorPrefs.SetString(ANDROID_SDK_FOLDER_KEY, ANDROID_SDK_FOLDER_PATH);

            // Get filename
            var path = Path.Combine(ANDROID_OUTPUT_PATH, ANDROID_APK);
            BuildPlayer(path, BuildTarget.Android);
        }

		static void StoreBuildNumber()
		{
			var jsonService = new JSONDataService ();
			var versionFilePath = BibaEditorConstants.GetResourceFilePath (BibaContentConstants.BIBAVERSION_FILE);
			var version = jsonService.ReadFromDisk<BibaVersion> (versionFilePath);
			version.BuildNumber = JenkinsBuildNumber.ToString();

			jsonService.WriteToDisk<BibaVersion> (version, versionFilePath);

			AssetDatabase.Refresh ();
		}

		static int JenkinsBuildNumber {
			get {

				int versionNumber = -1;
				Int32.TryParse(System.Environment.GetCommandLineArgs() [System.Environment.GetCommandLineArgs().Length - 2], out versionNumber);

				return versionNumber;
			}
		}

        static void BuildPlayer(string path, BuildTarget target)
        {
            // Create output directory
            Directory.CreateDirectory (Path.GetDirectoryName(path));
            
            // Build player
            BuildPipeline.BuildPlayer(GetScenes(), path, target, BuildOptions.None);
        }

		static string[] GetScenes()
		{
			var scenes = new List<string>();
			foreach (var scene in EditorBuildSettings.scenes)
			{
				if (scene == null)
				{
					continue;
				}
				if (scene.enabled)
				{
					scenes.Add(scene.path);
				}
			}
			return scenes.ToArray();
		}
	}
}