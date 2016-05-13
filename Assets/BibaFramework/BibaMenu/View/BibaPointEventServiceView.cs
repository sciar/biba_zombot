using System;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
	public class BibaPointEventServiceView : View
	{
		public PointEventService PointEventService;

		public void CheckForPointEvents()
		{
			PointEventService.CheckAndCompletePointEvent (PointEventKey);
		}
			
		[HideInInspector]
		public string PointEventKey;
	}
}