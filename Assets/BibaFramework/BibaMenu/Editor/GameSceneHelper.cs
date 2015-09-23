using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace BibaFramework.BibaMenuEditor
{
	public class GameSceneHelper : MonoBehaviour 
	{
		[MenuItem ("Biba/MenuState/Link Unity Scenes")]
		static void InitSceneHelper()
		{
			var window = EditorWindow.GetWindow<GameSceneHelperWindow> ();
			window.Show ();
		}
	}

	public class GameSceneHelperWindow : EditorWindow
	{
		private const string UNITY_EXTENSION = ".unity";
        private const string GAMESCENE = "GameScene";
		private const string GAMESCENES_FILE = GAMESCENE + ".cs";
        private const string NAMESPACE_GAMESCENE_FILE = "BibaFramework.BibaGame";

		private string _inputDir = string.Empty;
		private string _outputDir = string.Empty;

		void OnGUI()
		{
			//Input path
			GUILayout.Label ("Folder Path: " + _inputDir);
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Label ("Locate the Unity Game Scene directory");
			if (GUILayout.Button ("Select")) 
			{
				_inputDir = EditorUtility.OpenFolderPanel ("Select the directory", "", "");
			}
			EditorGUILayout.EndHorizontal ();

			GUILayout.Space (10);

			//Output path
			GUILayout.Label ("Folder Path: " + _outputDir);
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Label ("Locate the " + GAMESCENES_FILE + " directory");
			if (GUILayout.Button ("Select")) 
			{
				_outputDir = EditorUtility.OpenFolderPanel ("Select the directory", "", "");
			}
			EditorGUILayout.EndHorizontal ();

			var validPath = Directory.Exists(_inputDir) && _outputDir.StartsWith(Application.dataPath);

			GUI.enabled = validPath;
			if (GUILayout.Button ("Generate " + GAMESCENES_FILE)) 
			{
				GenerateGameSceneEnums();
				GenerateGameSceneBuildSettings();
			}
			GUI.enabled = true;
		}

		//Generating the GameScene.cs file
		void GenerateGameSceneEnums()
		{
            var outputPath = Path.Combine (_outputDir, GAMESCENES_FILE);
            var enums = UnitySceneFilePaths.Select(sceneFile => Path.GetFileNameWithoutExtension(sceneFile));

            HelperMethods.WriteEnumFile(NAMESPACE_GAMESCENE_FILE, GAMESCENE, enums.ToList(), outputPath);
			AssetDatabase.Refresh ();
		}

		//Adding scenes to BuildSettings
		void GenerateGameSceneBuildSettings()
		{
			var scenesToAdd = new List<EditorBuildSettingsScene> ();

			Array.ForEach (UnitySceneFilePaths, (sceneFilePath) => {

				//Remove the Application.dataPath/ from the sceneFilePath
				var pathToRemove = Directory.GetParent(Application.dataPath);
				sceneFilePath = sceneFilePath.Substring(pathToRemove.FullName.Length + 1);
				scenesToAdd.Add (new EditorBuildSettingsScene (sceneFilePath, true));
			});
			EditorBuildSettings.scenes = scenesToAdd.ToArray();

			AssetDatabase.Refresh ();
	    }

		string[] UnitySceneFilePaths { get { return Directory.GetFiles (_inputDir, "*" + UNITY_EXTENSION); } }
	}
}
