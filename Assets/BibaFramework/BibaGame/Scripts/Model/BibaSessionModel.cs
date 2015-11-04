using System.Collections.Generic;
using System;
using LitJson;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaSessionModel
    {
        public string UDID;
        public string DeviceModel;
        public string DeviceOS;
        public bool TagScanned { get; set; }
        public string QuadTileId { get; set; }

        public override string ToString()
        {
            return string.Format("[BibaSessionModel: UDID={0}, DeviceModel={1}, DeviceOS={2}, TagScanned={3}, QuadKey={4}]", UDID, DeviceModel, DeviceOS, TagScanned, QuadTileId);
        }
    }
}