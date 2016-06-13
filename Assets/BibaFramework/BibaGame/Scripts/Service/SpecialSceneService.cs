using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public class SpecialSceneService : BaseSettingsService<BibaSpecialSceneSettings>
    {
        [Inject]
        public BibaSessionModel BibaSessionModel { get; set; }

        public override string SettingsFileName {
            get {
                return BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE;
            }
        }

        #region - IContentUpdated
        public override void ReloadContent()
        {
            _settings = DataService.ReadFromDisk<BibaSpecialSceneSettings>(ContentFilePath);
        }
        #endregion

        public List<string> GetGeoBasedScenes()
        {
            return GetGeoBasedScenesSettings().Select(setting => setting.Id).ToList();
        }

        List<GeoSceneSetting> GetGeoBasedScenesSettings()
        {
			return Settings.GeoSceneSettings.Where(setting => DistanceTo(setting.Center.x, setting.Center.y, BibaSessionModel.RoundInfo.Location.x, BibaSessionModel.RoundInfo.Location.y) <= setting.Radius).ToList();
        }

        public string GetNextSceneToShow(string nextScene)
        {
            var result = CheckForGeoBasedScene(nextScene);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            return CheckForTimeBasedScene(nextScene);
        }

        string CheckForGeoBasedScene(string nextScene)
        {
            var result = GetGeoBasedScenesSettings().Find(setting => setting.SceneName == nextScene);
            return result != null ? result.Id : string.Empty;
        }

        string CheckForTimeBasedScene(string nextScene)
        {
            var result = Settings.TimedSceneSettings.Find(setting => setting.SceneName == nextScene && 
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