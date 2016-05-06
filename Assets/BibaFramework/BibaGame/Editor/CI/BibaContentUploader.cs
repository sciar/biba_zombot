using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BibaFramework.BibaGame;
using BibaFramework.BibaNetwork;
using BibaFramework.Utility;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaEditor
{
    public class BibaContentUploader : MonoBehaviour 
	{   
        [MenuItem ("Biba/Content Generation/Run Pipeline Without Upload", false, 0)]
        public static void RunContentPipeLineWithoutUpload()
        {
            ImportSettingsFromGoogleDocs();
            BibaAssetBundleBuilder.BuildSpecialScenesAssetBundles();
            UpdateManifestForContent();
            CopyContentToResources();
        }

        [MenuItem ("Biba/Content Generation/Run Pipeline With Upload", false, 1)]
        public static void RunContentPipeLine()
        {
            RunContentPipeLineWithoutUpload();
            UploadContentFolder();
        }

		[MenuItem ("Biba/Content Generation/Run Settings Pipeline Without Upload", false, 2)]
		public static void RunSettingsContentPipeLineWithoutUpload()
		{
			ImportSettingsFromGoogleDocs();
			UpdateManifestForContent();
			CopyContentToResources();
		}

		[MenuItem ("Biba/Content Generation/Run Settings Pipeline With Upload", false, 3)]
		public static void RunSettingsPipeLine()
		{
			RunSettingsContentPipeLineWithoutUpload();
			UploadContentFolder();
		}

		[MenuItem ("Biba/Content Generation/Run AssetBundle Pipeline Without Upload", false, 4)]
		public static void RunAssetBundlePipeLineWithoutUpload()
		{
			BibaSpecialSceneImporter.CreateSpecialSceneSettings();
			BibaAssetBundleBuilder.BuildSpecialScenesAssetBundles();
			UpdateManifestForContent();
			CopyContentToResources();
		}

		[MenuItem ("Biba/Content Generation/Run AssetBundle Pipeline With Upload", false, 5)]
		public static void RunAssetBundlePipeLine()
		{
			RunAssetBundlePipeLineWithoutUpload();
			UploadContentFolder();
		}

        static void CopyContentToResources()
        {
            var outputFolder = Path.GetDirectoryName(BibaEditorConstants.GetContentOutputPath(""));

			var resourceFolder = Path.GetDirectoryName(BibaEditorConstants.GetResourceFilePath(""));
            if (Directory.Exists(resourceFolder))
            {
                Directory.Delete(resourceFolder, true);
            }

			Directory.CreateDirectory (Application.dataPath + "/Resources/");
            FileUtil.CopyFileOrDirectory(outputFolder, resourceFolder);
            
            var filesToDelete = Directory.GetFiles(resourceFolder,"*", SearchOption.AllDirectories).Where(fileName => !fileName.EndsWith(BibaContentConstants.UNITY3D_EXTENSION) && 
                                                                                                          !fileName.EndsWith(BibaContentConstants.TEXT_EXTENSION)).ToList();
            foreach (var file in filesToDelete)
            {
                File.Delete(file);
            }

            AssetDatabase.Refresh();
        }

        static void ImportSettingsFromGoogleDocs()
        {
            BibaLocalizationImporter.CreateLocalizationSettings();
            BibaAchievementImporter.CreateAchievementSettings();
            BibaSpecialSceneImporter.CreateSpecialSceneSettings();
            AssetDatabase.Refresh();
        }

        static BibaManifest _bibaManifest;
        static BibaManifest BibaManifest
        {       
            get {
                if(_bibaManifest == null)
                {
					ReloadManifest ();
                }
                return _bibaManifest;
            }
        }

		static void ReloadManifest()
		{
			_bibaManifest = new JSONDataService().ReadFromDisk<BibaManifest>(BibaEditorConstants.GetContentOutputPath(BibaContentConstants.MANIFEST_FILENAME));

			if (_bibaManifest == null)
			{
				_bibaManifest = new BibaManifest();
			}
		}

        static void UpdateManifestForContent()
        {
			ReloadManifest ();

            var outputFolder = Path.GetDirectoryName(BibaEditorConstants.GetContentOutputPath(""));
            var manifestUpdated = false;
            var manifest = BibaManifest;
            var filesToUpload = Directory.GetFiles(outputFolder,"*", SearchOption.AllDirectories).Where(filePath => (filePath.EndsWith(BibaContentConstants.UNITY3D_EXTENSION) || filePath.EndsWith(BibaContentConstants.TEXT_EXTENSION)) &&
                                                                       !filePath.EndsWith(BibaContentConstants.MANIFEST_FILENAME));
            foreach (var filePath in filesToUpload)
            {
                var fileName = Path.GetFileName(filePath);
                var manifestLine = manifest.Lines.Find(line => line.FileName == fileName);
                if(manifestLine == null)
                {
                    manifestLine = new ManifestLine(){
                        FileName = fileName,
                        OptionalDownload = File.Exists(Path.Combine(BibaEditorConstants.OPTIONAL_ASSETBUNDLES_FOLDER, Path.GetFileNameWithoutExtension(fileName) + BibaEditorConstants.UNITY_EXTENSION))
                    };
                    manifest.Lines.Add(manifestLine);
                }
                
				var assetHashCode = BibaUtility.GetHashString(filePath);
                if(manifestLine.HashCode != assetHashCode)
                {
                    manifestLine.HashCode = assetHashCode;
					manifestLine.TimeStamp = DateTime.UtcNow;
                    
                    manifestUpdated = true;
                }
            }
            
            if (manifestUpdated)
            {
				BibaManifest.TimeStamp = DateTime.UtcNow;
                new JSONDataService().WriteToDisk<BibaManifest>(BibaManifest, BibaEditorConstants.GetContentOutputPath(BibaContentConstants.MANIFEST_FILENAME));
            }
            
            AssetDatabase.Refresh();
        }

        static void UploadContentFolder()
        {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = BibaEditorConstants.MONO_PATH;
                process.StartInfo.WorkingDirectory = Path.GetDirectoryName(BibaEditorConstants.S3UPLOADER_PATH);
				process.StartInfo.Arguments = Path.GetFileName(BibaEditorConstants.S3UPLOADER_PATH) + " " + BibaContentConstants.S3BucketName;
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