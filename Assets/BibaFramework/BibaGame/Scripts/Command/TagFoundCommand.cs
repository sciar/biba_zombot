using System;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class TagFoundCommand : Command
	{
		[Inject]
		public BibaTagType TagFound { get; set; }

		[Inject]
		public Transform TagTransform { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public TagScanCompletedSignal TagScanCompletedSignal { get; set; }

		public override void Execute ()
		{
			if (TagFound == BibaDeviceSession.TagToScan) 
			{
				BibaDeviceSession.TagScanned = true;
				BibaDeviceSession.TagTransform = TagTransform;

				TagScanCompletedSignal.Dispatch ();
			}
		}
	}
}