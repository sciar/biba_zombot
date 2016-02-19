using System.Collections;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using BibaFramework.BibaGame;
using BibaFramework.BibaNetwork;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaEditor
{
    public class BibaSpecialSceneAssetBundleBuilder : MonoBehaviour 
	{
        [MenuItem ("Biba/CI/AssetBundles/Build Special Scenes")]
		static void BuildSpecialScenes()
		{
            BuildSpecialScenesAssetBundles();
		}

        static void BuildSpecialScenesAssetBundles()
        {
            UpdateAssetBundleNameForSceneFiles();
            BuildAssetBundles();
            UpdateManifestForAssetBundles();

            AssetDatabase.Refresh();
        }

        static BibaManifest _bibaManifest;
        static BibaManifest BibaManifest
        {       
            get {
                
                if(_bibaManifest == null)
                {
                    var jsonDataService = new JSONDataService();
                    _bibaManifest = jsonDataService.ReadFromDisk<BibaManifest>(BibaEditorConstants.MANIFEST_PATH);
                    
                    if (_bibaManifest == null)
                    {
                        _bibaManifest = new BibaManifest();
                    }
                }
                return _bibaManifest;
            }
        }

        static void UpdateAssetBundleNameForSceneFiles()
        {
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
                }
            }
        }

        static void BuildAssetBundles()
        {
            Directory.CreateDirectory(BibaEditorConstants.BIBA_CONTENT_OUTPUT_PATH);
            BuildPipeline.BuildAssetBundles (BibaEditorConstants.BIBA_CONTENT_OUTPUT_PATH, BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        }

        static void UpdateManifestForAssetBundles()
        {
            var manifest = BibaManifest;
            var assetBundleFilePaths = Directory.GetFiles(BibaEditorConstants.BIBA_CONTENT_OUTPUT_PATH).Where(filePath => !filePath.Contains("."));
            foreach (var filePath in assetBundleFilePaths)
            {
                var shortFilePath = filePath.Replace(Application.dataPath, "Assets");
                var manifestLine = manifest.Lines.Find(line => line.FileName == shortFilePath);
                if(manifestLine == null)
                {
                    manifestLine = new ManifestLine(){
                        FileName = shortFilePath,
                        Version = 0
                    };
                    manifest.Lines.Add(manifestLine);
                }

                var assetBundleHash = GetHashString(filePath);
                if(manifestLine.HashCode != assetBundleHash)
                {
                    manifestLine.HashCode = assetBundleHash;
                    manifestLine.Version++;
                    new JSONDataService().WriteToDisk<BibaManifest>(BibaManifest, BibaEditorConstants.MANIFEST_PATH);
                }
             }
        }

        static string GetHashString(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return System.Text.Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }
	}
}