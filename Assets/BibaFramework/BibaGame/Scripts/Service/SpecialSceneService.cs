using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public class SpecialSceneService : IContentUpdated
    {
        [Inject]
        public IDataService LoaderService { get; set; }

        [Inject]
        public ICDNService CDNService { get; set; }

        private BibaSpecialSceneSettings _specialSceneSettings;

        private BibaSpecialSceneSettings SpecialSceneSettings
        {
            get
            {
                if (_specialSceneSettings == null)
                {
                    ReloadContent();
                }
                return _specialSceneSettings;
            }
        }

        #region - IContentUpdated
        public bool ShouldLoadFromResources 
        {
            get 
            {
                var persistedManifest = LoaderService.ReadFromDisk<BibaManifest>(BibaContentConstants.GetPersistedPath(BibaContentConstants.MANIFEST_FILENAME));
                var persistedManifestLine = persistedManifest.Lines.Find(line => line.FileName == BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE);
                
                var resourceManifest = LoaderService.ReadFromDisk<BibaManifest>(BibaContentConstants.GetResourceContentFilePath(BibaContentConstants.MANIFEST_FILENAME));
                var resourceManifestLine = resourceManifest.Lines.Find(line => line.FileName == BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE);
                
                return persistedManifestLine == null || persistedManifestLine.Version <= resourceManifestLine.Version;
            }
        }
        public string ContentFilePath 
        {
            get 
            {
                return ShouldLoadFromResources ? BibaContentConstants.GetResourceContentFilePath(BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE) : 
                    BibaContentConstants.GetPersistedPath(BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE);
            }
        }
        public void ReloadContent()
        {
            _specialSceneSettings = LoaderService.ReadFromDisk<BibaSpecialSceneSettings>(ContentFilePath);
        }
        #endregion

        public string LookForSpecialSceneToShow(string nextScene)
        {
            var result = CheckForLocaleBaseScene(nextScene);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }

            return CheckForTimedBasedScene(nextScene);
        }

        string CheckForTimedBasedScene(string nextScene)
        {
            var result = SpecialSceneSettings.TimeSpecialSceneSettings.Find(setting => setting.SceneName == nextScene && 
                DateTime.UtcNow >= setting.StartDate &&
                DateTime.UtcNow <= setting.EndDate);
            return result != null ? result.Id : string.Empty;
        }

        string CheckForLocaleBaseScene(string nextScene)
        {
            //TODO:implement
            return string.Empty;
        }
    }
}