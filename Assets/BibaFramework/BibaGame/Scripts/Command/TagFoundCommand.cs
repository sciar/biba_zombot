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

		[Inject]
		public TagLostSignal TagLostSignal { get; set; }

		private bool _tagLost;

		public override void Execute ()
		{
			if (TagFound == BibaDeviceSession.TagToScan) 
			{
				BibaDeviceSession.TagScanned = true;
				new Task (PlayTagScanCompleteAnimation (), true);
			} 
			else 
			{
				PlayIncorrectScanAnimation ();
			}
		}

		IEnumerator PlayTagScanCompleteAnimation()
		{
			TagLostSignal.AddListener (TagLost);
			_tagLost = false;

			var anim = TagObject.GetComponent<Animator> ();
			if (anim == null) 
			{
				TagScanCompletedSignal.Dispatch ();
			} 
			else 
			{
				anim.SetTrigger (MenuStateTrigger.Yes);
				yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(ACTIVE));
				while (!_tagLost && anim.GetCurrentAnimatorStateInfo (0).IsName (ACTIVE)) 
				{
					yield return new WaitForEndOfFrame();
				}

				if (!_tagLost) 
				{
					TagScanCompletedSignal.Dispatch ();
				}
			}

			TagLostSignal.RemoveListener (TagLost);
		}

		void TagLost(BibaTagType tagType, GameObject gameObject)
		{
			_tagLost = true;
		}

		void PlayIncorrectScanAnimation()
		{
			var anim = TagObject.GetComponent<Animator> ();
			if (anim != null) 
			{
				anim.SetTrigger (MenuStateTrigger.No);
			} 
		}
	}
}