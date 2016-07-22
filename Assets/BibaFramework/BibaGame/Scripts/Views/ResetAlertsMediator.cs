using System;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaGame
{
	public class ResetAlertsMediator : Mediator
	{
		[Inject]
		public ResetAlertsView ResetAlertsView { get; set; }

		[Inject]
		public ResetDeviceSignal ResetDeviceSignal { get; set; }

		public override void OnRegister ()
		{
			ResetAlertsView.ResetPlayerSignal.AddListener (ResetPlayer);
		}

		public override void OnRemove ()
		{
			ResetAlertsView.ResetPlayerSignal.RemoveListener (ResetPlayer);
		}

		void ResetPlayer()
		{
			ResetDeviceSignal.Dispatch ();
		}
	}
}