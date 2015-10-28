using System.Collections.Generic;
using System;
using LitJson;

namespace BibaFramework.BibaGame
{
    public class BibaSessionModel
    {
        [JsonIgnore]
        public bool TagScanned { get; set; }

        public BibaSessionModel()
        {
        }
    }
}