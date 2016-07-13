using System.Collections.Generic;
using System;
using LitJson;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaSession
    {
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public bool TagScanned { get; set; }
		public bool TagEnabled { get; set; }
		public List<BibaEquipment> SelectedEquipments { get; set; }

		public string QuadTileId { get; set; }
		public Vector2 Location { get; set; }

		public DateTime LScoreStartTime { get; set; }
		public DateTime MScoreStartTime { get; set; }
		public DateTime VScoreStartTime { get; set; }

		public BibaSession()
		{
			SelectedEquipments = new List<BibaEquipment> ();
		}
    }
}