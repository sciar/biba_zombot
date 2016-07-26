using System;
using strange.extensions.command.impl;
using UnityEngine;
using System.Collections;
using BibaFramework.Utility;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class TagFoundCommand : Command
	{
		private const string ACTIVE = "Active";

		[Inject]
		public BibaTagType TagFound { get; set; }

		[Inject]
		public GameObject TagObject { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public TagScanCompletedSignal TagScanCompletedSignal { get; set; }

		public override void Execute ()
		{
			if (TagFound == BibaDeviceSession.TagToScan) 
			{
				BibaDeviceSession.TagScanned = true;
				new Task (PlayTagScanCompleteAnimation(), true);
			}
		}

		IEnumerator PlayTagScanCompleteAnimation()
		{
			var anim = TagObject.GetComponent<Animator> ();
			if (anim == null) 
			{
				TagScanCompletedSignal.Dispatch ();
			} 
			else 
			{
				anim.SetTrigger (MenuStateTrigger.Next);
				yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(ACTIVE));
				while (anim.GetCurrentAnimatorStateInfo (0).IsName (ACTIVE)) 
				{
					yield return new WaitForEndOfFrame();
				}
				TagScanCompletedSignal.Dispatch ();
			}
		}
	}
}