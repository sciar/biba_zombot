using System.Collections.Generic;
using System;
using LitJson;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaSession
    {
		public DateTime Start { get; set; }
		public DateTime End { get; set; }

		public bool TagScanned { get; set; }
		public bool TagEnabled { get; set; }
		public List<BibaEquipment> SelectedEquipments { get; set; }

		public string QuadTileId { get; set; }
		public Vector2 Location { get; set; }

		public DateTime LScoreStart { get; set; }
		public DateTime MScoreStart { get; set; }
		public DateTime VScoreStart { get; set; }

		public BibaSession()
		{
			Start = DateTime.UtcNow;
			SelectedEquipments = new List<BibaEquipment> ();
		}
    }
}