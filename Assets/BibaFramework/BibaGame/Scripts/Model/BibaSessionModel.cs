using System.Collections.Generic;
using System;
using LitJson;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaSessionModel
    {
		public SessionInfo SessionInfo { get; set; }

        public bool TagScanned { get; set; }
		public bool TagEnabled { get; set; }

		public DateTime LastPlayedTime { get; set; }
		public DateTime LastChartBoostTime { get; set; }
		public DateTime LastCameraReminderTime { get; set; }

		public List<BibaEquipment> SelectedEquipments { get; set; }

		public BibaSessionModel()
		{
			Reset ();
		}

		public void Reset()
		{
			LastPlayedTime = DateTime.MaxValue;
			LastChartBoostTime = DateTime.MinValue;
			LastCameraReminderTime = DateTime.MinValue;

			SelectedEquipments = new List<BibaEquipment> ();

			TagScanned = false;
			TagEnabled = false;
		}
    }

	public class SessionInfo	
	{
		public string UUID { get; set; }
        public string SessionID { get; set; }
		public string DeviceModel { get; set; }
		public string DeviceOS { get; set; }
        public string QuadTileId { get; set; }
        public Vector2 Location { get; set; }
	}
}