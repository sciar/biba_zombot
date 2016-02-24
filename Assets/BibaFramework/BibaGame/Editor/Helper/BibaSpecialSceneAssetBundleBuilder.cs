using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
            UploadManifestAndBundle();
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
                        _bibaManifest.Version = 0;
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
            Directory.CreateDirectory(BibaEditorConstants.BIBA_CONTENT_OUTPUT_PATH);
            BuildPipeline.BuildAssetBundles (BibaEditorConstants.BIBA_CONTENT_OUTPUT_PATH, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
       
            //Rename the built bundles
            var builtBundleFiles = Directory.GetFiles(BibaEditorConstants.BIBA_CONTENT_OUTPUT_PATH).Where(filePath => !filePath.Contains(".")).ToList();
            builtBundleFiles.ForEach((file) => {
                var newPath = file + BibaContentConstants.UNITY3D_EXTENSION;
                if(File.Exists(newPath))
                {
                    File.Delete(newPath);
                }
                File.Move(file, newPath);
            });
        }

        static void UpdateManifestForAssetBundles()
        {
            var manifestUpdated = false;
            var manifest = BibaManifest;
            var assetBundleFiles = Directory.GetFiles(BibaEditorConstants.BIBA_CONTENT_OUTPUT_PATH).Where(filePath => filePath.EndsWith(BibaContentConstants.UNITY3D_EXTENSION));
            foreach (var filePath in assetBundleFiles)
            {
                var fileName = Path.GetFileName(filePath);
                var manifestLine = manifest.Lines.Find(line => line.FileName == fileName);
                if(manifestLine == null)
                {
                    manifestLine = new ManifestLine(){
                        FileName = fileName,
                        Version = 0
                    };
                    manifest.Lines.Add(manifestLine);
                }

                var assetBundleHash = GetHashString(filePath);
                if(manifestLine.HashCode != assetBundleHash)
                {
                    manifestLine.HashCode = assetBundleHash;
                    manifestLine.Version++;

                    manifestUpdated = true;
                }
             }

            if (manifestUpdated)
            {
                BibaManifest.Version++;
                new JSONDataService().WriteToDisk<BibaManifest>(BibaManifest, BibaEditorConstants.MANIFEST_PATH);
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

        static void UploadManifestAndBundle()
        {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = BibaEditorConstants.MONO_PATH;
                process.StartInfo.WorkingDirectory = Path.GetDirectoryName(BibaEditorConstants.S3UPLOADER_PATH);
                process.StartInfo.Arguments = Path.GetFileName(BibaEditorConstants.S3UPLOADER_PATH) + " " + BibaContentConstants.CI_GAME_ID;
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;
                process.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
	}
}