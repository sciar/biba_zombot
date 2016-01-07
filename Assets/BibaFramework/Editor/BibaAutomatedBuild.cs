using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace BibaFramework.BibaMenuEditor
{
	public static class BibaAutomatedBuild
	{
		private const string IOS_OUTPUT_PATH = "Build/iOS";
		private const string ANDROID_OUTPUT_PATH = "Build/Android";
		private const string ANDROID_APK = "android.apk";

		[MenuItem("Biba/CI/Build iOS")]
		public static void BuildIOS ()
		{
			// Get filename
			var path = IOS_OUTPUT_PATH;

			// Create output directory
			Directory.CreateDirectory (path);

            // Set Build Number
            PlayerSettings.iOS.buildNumber = Environment.GetCommandLineArgs() [System.Environment.GetCommandLineArgs().Length - 1];

			// Build player
			BuildPipeline.BuildPlayer(GetScenes(), path, BuildTarget.iOS, BuildOptions.None);
		}

		[MenuItem("Biba/CI/Build Android")]
		public static void BuildAndroid ()
		{
			// Get filename
			var path = ANDROID_OUTPUT_PATH;
			
			// Create output directory
			Directory.CreateDirectory (path);
			
            // Set Build Number
            int versionNumber = -1;
            Int32.TryParse(Environment.GetCommandLineArgs() [System.Environment.GetCommandLineArgs().Length - 1], out versionNumber);

            PlayerSettings.Android.bundleVersionCode = versionNumber;

			// Build player
			BuildPipeline.BuildPlayer(GetScenes(), Path.Combine(path, ANDROID_APK), BuildTarget.Android, BuildOptions.None);
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