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

        [Inject]
        public BibaSessionModel BibaSessionModel { get; set; }

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
                if(persistedManifest == null)
                {
                    return true;
                }

                var persistedManifestLine = persistedManifest.Lines.Find(line => line.FileName == BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE);
                if(persistedManifestLine == null)
                {
                    return true;
                }

                var resourceManifest = LoaderService.ReadFromDisk<BibaManifest>(BibaContentConstants.GetResourceContentFilePath(BibaContentConstants.MANIFEST_FILENAME));
                var resourceManifestLine = resourceManifest.Lines.Find(line => line.FileName == BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE);
                if(resourceManifestLine == null)
                {
                    return true;
                }
                return persistedManifestLine.Version <= resourceManifestLine.Version;
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
            Debug.Log(result);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            return CheckForTimedBasedScene(nextScene);
        }

        string CheckForLocaleBaseScene(string nextScene)
        {
            var result = SpecialSceneSettings.LocaleSceneSettings.Find(setting => setting.SceneName == nextScene &&
                                                                       DistanceTo(setting.Center.x, setting.Center.y, BibaSessionModel.SessionInfo.Location.x, BibaSessionModel.SessionInfo.Location.y) <= setting.Radius);
            return result != null ? result.Id : string.Empty;
        }

        string CheckForTimedBasedScene(string nextScene)
        {
            var result = SpecialSceneSettings.TimedSceneSettings.Find(setting => setting.SceneName == nextScene && 
                DateTime.UtcNow >= setting.StartDate &&
                DateTime.UtcNow <= setting.EndDate);
            return result != null ? result.Id : string.Empty;
        }

        double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
            double rlat1 = Math.PI*lat1/180;
            double rlat2 = Math.PI*lat2/180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI*theta/180;
            double dist = 
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * 
                    Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist*180/Math.PI;
            dist = dist*60*1.1515;
            
            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }
            
            return dist;
        }
    }
}