using System;
using strange.extensions.command.impl;
using UnityEngine;
using System.Collections;
using BibaFramework.Utility;
using BibaFramework.BibaMenu;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
	public class TagFoundCommand : Command
	{
		private const string ACTIVE = "Active";
		private const float COMPLETE_WAITTIME = 0.25f;

		[Inject]
		public BibaTagType TagFound { get; set; }

		[Inject]
		public GameObject TagObject { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public TagScanCompletedSignal TagScanCompletedSignal { get; set; }

		[Inject]
		public IDeviceAnalyticService DeviceAnalyticService { get; set; }

		[Inject]
		public ToggleTagSignal ToggleTagSignal { get; set; }

		public override void Execute ()
		{
            if (IsCorrectTag) 
			{
				BibaDeviceSession.TagScanned = true;
				ToggleTagSignal.Dispatch (false);
				new Task (PlayTagScanCompleteAnimation (), true);
			} 
			else 
			{
				PlayIncorrectScanAnimation ();
			}
		}

        protected virtual bool IsCorrectTag {
            get {
                return TagFound == BibaDeviceSession.TagToScan;
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
				anim.SetTrigger (MenuStateTrigger.Yes);
				ProcessUnlockable ();

				yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(ACTIVE));
				while (anim.GetCurrentAnimatorStateInfo (0).IsName (ACTIVE)) 
				{
					yield return new WaitForEndOfFrame();
				}

				yield return new WaitForSeconds (COMPLETE_WAITTIME);
				TagScanCompletedSignal.Dispatch ();
				TagObject.GetComponent<BibaTagEventHandlerView> ().ARUnlock.Reset ();
			}

			DeviceAnalyticService.TrackTagScanned ();
		}
			
		void PlayIncorrectScanAnimation()
		{
			var anim = TagObject.GetComponent<Animator> ();
			if (anim != null) 
			{
				anim.SetTrigger (MenuStateTrigger.No);
			} 
		}

		void ProcessUnlockable()
		{
			//TODO: Override for each game
		}

		void SetUnlockableAtView(string unlockedItem)
		{
			if (!string.IsNullOrEmpty(unlockedItem) ) 
			{
				var tagEventView = TagObject.GetComponent<BibaTagEventHandlerView> ();
				tagEventView.SetUnlockedSprite (unlockedItem);
			}
		}
	}
}