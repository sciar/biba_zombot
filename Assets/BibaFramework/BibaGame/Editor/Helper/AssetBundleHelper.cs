using UnityEngine;
using UnityEditor;
using BibaFramework.BibaGame;
using BibaFramework.BibaNetwork;
using System.IO;

namespace BibaFramework.BibaMenuEditor
{
    public class AssetBundleHelper : MonoBehaviour 
	{
        [MenuItem ("Biba/CI/AssetBundles/Build Special Scenes")]
		static void BuildSpecialScenes()
		{
            BuildSpecialScenesAssetBundles();
		}

        static void BuildSpecialScenesAssetBundles()
        {
            Directory.CreateDirectory(BibaEditorConstants.SCENE_ASSETBUNDLES_OUTPUT_PATH);

            var jsonDataService = new JSONDataService();
            var manifest = jsonDataService.ReadFromDisk<BibaManifest>(BibaEditorConstants.MANIFEST_PATH);
            if (manifest == null)
            {
                manifest = new BibaManifest();
            }

            var sceneFilePaths = Directory.GetFiles(BibaEditorConstants.SCENE_ASSETBUNDLES_INPUT_PATH);
            foreach (var filePath in sceneFilePaths)
            {
                var shortFilePath = filePath.Replace(Application.dataPath, "Assets");
                if(shortFilePath.EndsWith(BibaEditorConstants.UNITY3D_EXTENSION))
                {
                    var sceneFileName = Path.GetFileNameWithoutExtension(shortFilePath);

                    var assetImporter = AssetImporter.GetAtPath(shortFilePath);
                    assetImporter.assetBundleName = sceneFileName;
                    assetImporter.SaveAndReimport();

                    var manifestLine = manifest.Lines.Find(line => line.FileName == sceneFileName);
                    if(manifestLine == null)
                    {
                        manifestLine = new ManifestLine(){
                            FileName = sceneFileName,
                            Version = 0
                        };
                        manifest.Lines.Add(manifestLine);
                    }
                    manifestLine.Version++;

                }
             }

            jsonDataService.WriteToDisk<BibaManifest>(manifest, BibaEditorConstants.MANIFEST_PATH);

            BuildPipeline.BuildAssetBundles (BibaEditorConstants.SCENE_ASSETBUNDLES_OUTPUT_PATH, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
	}
}