using System;
using System.Linq;
using System.Collections.Generic;
using LitJson;

namespace BibaFramework.BibaGame
{
    public class BibaAchievement
    {
        public string  Id { get; private set; }
        public bool Shown { get; set; }

        [JsonIgnore]
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