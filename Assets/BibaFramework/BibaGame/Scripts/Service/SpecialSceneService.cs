using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public class SpecialSceneService
    {
        [Inject]
        public IDataService LoaderService { get; set; }

        private BibaSpecialSceneSettings _specialSceneSettings;
        private BibaSpecialSceneSettings SpecialSceneSettings {
            get 
            {
                if(_specialSceneSettings == null)
                {
                    _specialSceneSettings = LoaderService.ReadFromDisk<BibaSpecialSceneSettings>(BibaDataConstants.SPECIAL_SCENE_SETTINGS_PATH);
                }
                return _specialSceneSettings;
            }
        }

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