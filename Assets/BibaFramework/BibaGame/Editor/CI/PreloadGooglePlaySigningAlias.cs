using UnityEngine;
using UnityEditor;
using System.IO;

namespace BibaFramework.BibaEditor
{
	[InitializeOnLoad]
	public class PreloadGooglePlaySigningAlias
	{
		private static string ANDROID_SIGNING_PASSWORD = "#sling2Rock!";
		static PreloadGooglePlaySigningAlias ()
		{
			PlayerSettings.Android.keystorePass = ANDROID_SIGNING_PASSWORD;
			PlayerSettings.Android.keyaliasName = PlayerSettings.Android.keyaliasName;
			PlayerSettings.Android.keyaliasPass = ANDROID_SIGNING_PASSWORD;
		}
	}
}