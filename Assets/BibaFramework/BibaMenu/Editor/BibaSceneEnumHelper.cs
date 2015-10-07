using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaMenuEditor
{
	public class BibaSceneEnumHelper : MonoBehaviour 
	{
		[MenuItem ("Biba/Create BibaScene Enum from Unity Scenes")]
		static void InitSceneHelper()
		{
			var window = EditorWindow.GetWindow<BibaSceneEnumHelperWindow> ();
			window.Show ();
		}
	}

	public class BibaSceneEnumHelperWindow : EditorWindow
	{
		protected string _inputDir = string.Empty;
		private string _outputDir = string.Empty;

		void OnGUI()
		{
			//Input path
			GUILayout.Label ("Folder Path: " + _inputDir);
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Label ("Locate the Input directory");
			if (GUILayout.Button ("Select")) 
			{
				_inputDir = EditorUtility.OpenFolderPanel ("Select the directory", "", "");
			}
			EditorGUILayout.EndHorizontal ();

			GUILayout.Space (10);

			//Output path
			GUILayout.Label ("Folder Path: " + _outputDir);
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Label ("Locate the " + OutputFileName + " directory");
			if (GUILayout.Button ("Select")) 
			{
				_outputDir = EditorUtility.OpenFolderPanel ("Select the directory", "", "");
			}
			EditorGUILayout.EndHorizontal ();

			var validPath = Directory.Exists(_inputDir) && _outputDir.StartsWith(Application.dataPath);

			GUI.enabled = validPath;
			if (GUILayout.Button ("Generate " + OutputFileName)) 
			{
				GenerateGameSceneEnums();
                GenerateAdditionalSettings();
			}
			GUI.enabled = true;
		}


        //Generating the GameScene.cs file
        void GenerateGameSceneEnums()
        {
            var outputPath = Path.Combine (_outputDir, OutputFileName);

            HelperMethods.WriteEnumFile(OutputNameSpaceName, OutputClassName, EnumStrings.ToList(), outputPath);
            AssetDatabase.Refresh ();
        }

        protected virtual void GenerateAdditionalSettings()
        {
            //Adding scenes to BuildSettings
            var scenesToAdd = new List<EditorBuildSettingsScene> ();
            
            Array.ForEach (UnitySceneFiles, (sceneFilePath) => {
                
                //Remove the Application.dataPath/ from the sceneFilePath
                var pathToRemove = Directory.GetParent(Application.dataPath);
                sceneFilePath = sceneFilePath.Substring(pathToRemove.FullName.Length + 1);
                scenesToAdd.Add (new EditorBuildSettingsScene (sceneFilePath, true));
            });

            var startSceneIndex = scenesToAdd.FindIndex(scene => scene.path.EndsWith(BibaMenuConstants.FIRST_SCENE, StringComparison.InvariantCultureIgnoreCase));
            if (startSceneIndex != -1)
            {
                var tempScene = scenesToAdd[0];
                scenesToAdd[0] = scenesToAdd[startSceneIndex];
                scenesToAdd[startSceneIndex] = tempScene;
            }

            EditorBuildSettings.scenes = scenesToAdd.ToArray();
            
            AssetDatabase.Refresh ();
        }
        private string[] UnitySceneFiles { get { return Directory.GetFiles(_inputDir, "*.unity"); } }

        protected virtual string[] EnumStrings { get { return UnitySceneFiles.Select(sceneFile => Path.GetFileNameWithoutExtension(sceneFile)).ToArray(); } }
        protected virtual string OutputFileName { get { return "BibaScene.cs"; } }
        protected virtual string OutputClassName { get { return "BibaScene"; } }
        protected virtual string OutputNameSpaceName { get { return "BibaFramework.BibaGame"; } }
	}
}
