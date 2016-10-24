using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BibaFramework.BibaMenu;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaEditor
{
	public class BibaSceneEnumHelper : MonoBehaviour 
	{
        [MenuItem ("Biba/Constants and Enums/BibaScene Constants")]
		static void InitSceneHelper()
		{
			var window = EditorWindow.GetWindow<BibaSceneEnumHelperWindow> ();
			window.Show ();
		}
	}

	public class BibaSceneEnumHelperWindow : BibaEnumHelper
	{
        private string[] UnitySceneFiles { get { return Directory.GetFiles(_inputDir, "*.unity"); } }
        protected override List<string> EnumStrings { get { return UnitySceneFiles.Select(sceneFile => Path.GetFileNameWithoutExtension(sceneFile)).ToList(); } }
        protected override string OutputFileName { get { return "BibaScene.cs"; } }
        protected override string OutputClassName { get { return "BibaScene"; } }
        protected override string OutputNameSpaceName { get { return "BibaFramework.BibaGame"; } }

        protected override void GenerateAdditionalSettings()
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

        protected override void WriteToFile (string outputPath)
        {
            WriteConstStringToFile(outputPath);
        }
	}
}