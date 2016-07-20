using System.Collections.Generic;
using System;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaDeviceSession
    {
		public DateTime Start { get; set; }
		public DateTime End { get; set; }

		public string QuadTileId { get; set; }
		public Vector2 Location { get; set; }

		public bool TagEnabled { get; set; }
		public bool TagScanned { get; set; }

		public List<BibaEquipment> SelectedEquipments { get; set; }

		public BibaDeviceSession()
		{
			Start = DateTime.UtcNow;
			SelectedEquipments = new List<BibaEquipment> ();
		}
    }
}