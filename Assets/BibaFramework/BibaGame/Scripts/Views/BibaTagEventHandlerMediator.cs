using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class BibaTagEventHandlerMediator : Mediator
	{
		[Inject]
		public BibaTagEventHandlerView BibaTagEventHandlerView { get; set; }

		[Inject]
		public ToggleTagScanSignal ToggleTagScanSignal { get; set; }

		[Inject]
		public TagFoundSignal TagFoundSignal { get; set; }

		[Inject]
		public TagLostSignal TagLostSignal { get; set; }

		public override void OnRegister ()
		{
			ToggleTagScanSignal.AddListener (ToggleTagScan);
		}

		public override void OnRemove ()
		{
			ToggleTagScanSignal.RemoveListener (ToggleTagScan);
		}

		void ToggleTagScan(bool status)
		{
			if (status) 
			{
				BibaTagEventHandlerView.TrackingFoundSignal.AddListener (TrackingFound);
			} 
			else 
			{
				BibaTagEventHandlerView.TrackingFoundSignal.RemoveListener (TrackingFound);
			}
		}

		void TrackingFound(string fileName, Transform tagTransform)
		{
			if (Enum.IsDefined (typeof(BibaTagType), fileName)) 
			{
				TagFoundSignal.Dispatch ((BibaTagType)Enum.Parse (typeof(BibaTagType), fileName), BibaTagEventHandlerView.transform);
			}
		}

		void TrackingLost(string fileName, Transform tagTransform)
		{
			if(Enum.IsDefined(typeof(BibaTagType), fileName))
			{
				TagLostSignal.Dispatch((BibaTagType)Enum.Parse(typeof(BibaTagType), fileName), BibaTagEventHandlerView.transform);
			}
		}
	}
}