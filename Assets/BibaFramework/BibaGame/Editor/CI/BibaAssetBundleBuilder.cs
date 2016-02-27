using System.IO;
using System.Linq;
using BibaFramework.BibaNetwork;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaEditor
{
    public class BibaAssetBundleBuilder : MonoBehaviour 
	{
        [MenuItem ("Biba/Content Generation/Build Special Scenes")]
		static void BuildSpecialScenes()
		{
            BuildSpecialScenesAssetBundles();
		}

        static void BuildSpecialScenesAssetBundles()
        {
            UpdateAssetBundleNameForSceneFiles();
            BuildAssetBundles();
        }

        static void UpdateAssetBundleNameForSceneFiles()
        {
            var sceneFilePaths = Directory.GetFiles(BibaEditorConstants.SCENE_ASSETBUNDLES_INPUT_PATH);
            foreach (var filePath in sceneFilePaths)
            {
                var shortFilePath = filePath.Replace(Application.dataPath, "Assets");
                if(shortFilePath.EndsWith(BibaEditorConstants.UNITY_EXTENSION))
                {
                    var sceneFileName = Path.GetFileNameWithoutExtension(shortFilePath);

                    var assetImporter = AssetImporter.GetAtPath(shortFilePath);
                    assetImporter.assetBundleName = sceneFileName;
                    assetImporter.SaveAndReimport();
                }
            }
        }

        static void BuildAssetBundles()
        {
            var outputFolder = Path.GetDirectoryName(BibaEditorConstants.GetContentOutputPath(""));
            Directory.CreateDirectory(outputFolder);
            BuildPipeline.BuildAssetBundles (outputFolder, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
       
            //Rename the built bundles
            var builtBundleFiles = Directory.GetFiles(outputFolder).Where(filePath => !filePath.Contains(".")).ToList();
            builtBundleFiles.ForEach((file) => {
                var newPath = file + BibaContentConstants.UNITY3D_EXTENSION;
                if(File.Exists(newPath))
                {
                    File.Delete(newPath);
                }
                File.Move(file, newPath);
            });
        }
	}
}