using System;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaSpecialSceneSettings
    {
        public List<BibaSpecialSceneSetting> SpcialSceneSettings = new List<BibaSpecialSceneSetting>();
    }

    public class BibaSpecialSceneSetting
    { 
        public string Id;
        public string SceneName;
    }

    public class TimedSceneSetting : BibaSpecialSceneSetting
    {  
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}