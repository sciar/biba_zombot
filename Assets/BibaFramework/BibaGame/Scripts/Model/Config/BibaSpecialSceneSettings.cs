using System;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	[Serializable]
    public class BibaSpecialSceneSettings
    {
        public List<TimedSceneSetting> TimedSceneSettings = new List<TimedSceneSetting>();
        public List<GeoSceneSetting> GeoSceneSettings = new List<GeoSceneSetting>();
    }

	[Serializable]
    public class BibaSpecialSceneSetting
    { 
        public string Id;
        public string SceneName;

        public override string ToString()
        {
            return string.Format("[BibaSpecialSceneSetting: Id={0}, SceneName={1}]", Id, SceneName);
        }
    }

	[Serializable]
    public class TimedSceneSetting : BibaSpecialSceneSetting
    {  
		public DateTime StartDate;
		public DateTime EndDate;

        public override string ToString()
        {
            return string.Format("[TimedSceneSetting: StartDate={0}, EndDate={1}]", StartDate, EndDate);
        }  
    }

	[Serializable]
    public class GeoSceneSetting : BibaSpecialSceneSetting
    {
		public Vector2 Center;
		public double Radius;
    }
}