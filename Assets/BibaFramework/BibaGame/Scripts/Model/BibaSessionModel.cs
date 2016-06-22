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
			SessionInfo = new SessionInfo ();
		}
    }

	public class SessionInfo	
	{
		public string UUID { get; set; }
		public string DeviceModel { get; set; }
		public string DeviceOS { get; set; }

		//in seconds
		public float LightActivityTime { get; set; }
		public float ModerateActivityTime { get; set; }
		public float VigorousActivityTime { get; set; }
		
		public DateTime LightTrackingStartTime { get; set; }
		public DateTime ModerateTrackingStartTime { get; set; }
		public DateTime VigorousTrackingStartTime { get; set; }
	}

	public class RoundInfo
	{
		public bool TagScanned { get; set; }
		public string QuadTileId { get; set; }
		public Vector2 Location { get; set; }
	}
}