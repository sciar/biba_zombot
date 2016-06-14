using System.Collections.Generic;
using System;
using LitJson;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaSessionModel
    {
		public SessionInfo SessionInfo { get; set; }
		public RoundInfo RoundInfo { get; set; }

		public BibaSessionModel()
		{
			Reset ();
		}

		public void Reset()
		{
			RoundInfo = new RoundInfo ();
		}
    }

	public class SessionInfo	
	{
		public string UUID { get; set; }
		public string DeviceModel { get; set; }
		public string DeviceOS { get; set; }
	}

	public class RoundInfo
	{
		public bool TagScanned { get; set; }

		public string QuadTileId { get; set; }
		public Vector2 Location { get; set; }

		//in seconds
		public float SedentaryTime { get; set; }
		public float ModerateTime { get; set; }
		public float VigorousTime { get; set; }

		public DateTime SedentaryTrackingStartTime { get; set; }
		public DateTime ModerateTrackingStartTime { get; set; }
		public DateTime VigorousTrackingStartTime { get; set; }
	}
}