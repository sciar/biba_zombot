using System;
using System.Linq;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	[Serializable]
    public class BibaAchievement
    {
		public string Id;
		public bool Shown;

        public BibaAchievementConfig Config;

        public BibaAchievement()
        {
        }

        public BibaAchievement(BibaAchievementConfig config)
        {
            Id = config.Id;
            Config = config;
        }
    }
}