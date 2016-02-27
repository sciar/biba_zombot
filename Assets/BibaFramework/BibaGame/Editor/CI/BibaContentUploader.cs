using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BibaFramework.BibaGame;
using BibaFramework.BibaNetwork;
using UnityEditor;
using UnityEngine;
using System.Security.Cryptography;

namespace BibaFramework.BibaEditor
{
    public class BibaContentUploader : MonoBehaviour 
	{
        [MenuItem ("Biba/Content Generation/Upload Content to S3")]
		static void UploadContent()
		{
            UploadManifestAndBundle();
		}

        [MenuItem ("Biba/Content Generation/Copy Content to Resources")]
        static void CopyToResources()
        {
            CopyContentToResources();
            AssetDatabase.Refresh();
        }

        static void CopyContentToResources()
        {
            var outputFolder = Path.GetDirectoryName(BibaEditorConstants.GetContentOutputPath(""));
            var resourceFolder = Path.GetDirectoryName(BibaContentConstants.GetResourceContentFilePath(""));
            Directory.Delete(resourceFolder, true);
            FileUtil.CopyFileOrDirectory(outputFolder, resourceFolder);

            var filesToDelete = Directory.GetFiles(resourceFolder,"*", SearchOption.AllDirectories).Where(fileName => !fileName.EndsWith(BibaContentConstants.UNITY3D_EXTENSION) && !fileName.EndsWith(BibaContentConstants.TEXT_EXTENSION)).ToList();
            foreach (var file in filesToDelete)
            {
                File.Delete(file);
            }
        }

        static BibaManifest _bibaManifest;
        static BibaManifest BibaManifest
        {       
            get {
                if(_bibaManifest == null)
                {
                    _bibaManifest = new JSONDataService().ReadFromDisk<BibaManifest>(BibaEditorConstants.GetContentOutputPath(BibaContentConstants.MANIFEST_FILENAME));
                    
                    if (_bibaManifest == null)
                    {
                        _bibaManifest = new BibaManifest();
                    }
                }
                return _bibaManifest;
            }
        }

        public static void UploadManifestAndBundle()
        {
            UpdateManifestForAssetBundles();
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

        static void UpdateManifestForAssetBundles()
        {
            var outputFolder = Path.GetDirectoryName(BibaEditorConstants.GetContentOutputPath(""));
            var manifestUpdated = false;
            var manifest = BibaManifest;
            var filesToUpload = Directory.GetFiles(outputFolder).Where(filePath => filePath.EndsWith(BibaContentConstants.UNITY3D_EXTENSION) || filePath.EndsWith(BibaContentConstants.TEXT_EXTENSION));
            foreach (var filePath in filesToUpload)
            {
                var fileName = Path.GetFileName(filePath);
                var manifestLine = manifest.Lines.Find(line => line.FileName == fileName);
                if(manifestLine == null)
                {
                    manifestLine = new ManifestLine(){
                        FileName = fileName,
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
                new JSONDataService().WriteToDisk<BibaManifest>(BibaManifest, BibaEditorConstants.GetContentOutputPath(BibaContentConstants.MANIFEST_FILENAME));
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