using UnityEngine;
using System;

namespace BibaFramework.BibaGame
{
    public class BibaSeasonalAchievementConfig : BibaAchievementConfig
    {   
        public Vector3 StartDateVector;
        public DateTime StartDate { get { return new DateTime((int)StartDateVector.x, (int)StartDateVector.y, (int)StartDateVector.z); } }

        public Vector3 EndDateVector;
        public DateTime EndDate { get { return new DateTime((int)EndDateVector.x, (int)EndDateVector.y, (int)EndDateVector.z); } }
    }
}