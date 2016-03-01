using System;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaSpecialSceneSettings
    {
        public List<TimedSceneSetting> TimedSceneSettings = new List<TimedSceneSetting>();
        public List<LocaleSceneSetting> LocaleSceneSettings = new List<LocaleSceneSetting>();
    }

    public class BibaSpecialSceneSetting
    { 
        public string Id;
        public string SceneName;

        public override string ToString()
        {
            return string.Format("[BibaSpecialSceneSetting: Id={0}, SceneName={1}]", Id, SceneName);
        }
    }

    public class TimedSceneSetting : BibaSpecialSceneSetting
    {  
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return string.Format("[TimedSceneSetting: StartDate={0}, EndDate={1}]", StartDate, EndDate);
        }  
    }

    public class LocaleSceneSetting : BibaSpecialSceneSetting
    {
        public Vector2 Center { get; set; }
        public double Radius { get; set; }

        public LocaleSceneSetting()
        {
            Radius = BibaGameConstants.LOCALE_BASED_SCENE_SETTING_RADIUS;
        }
    }
}