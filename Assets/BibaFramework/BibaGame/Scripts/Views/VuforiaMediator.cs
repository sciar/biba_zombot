using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class VuforiaMediator : Mediator
	{
		[Inject]
		public VuforiaView VuforiaView { get; set; }

		[Inject]
		public TagInitFailedSignal TagInitFailedSignal { get; set; }

		[Inject]
		public ToggleARCameraSignal ToggleARCameraSignal { get; set; }

		public override void OnRegister ()
		{
			ToggleTagScan (false);
			VuforiaView.VuforiaBehaviour.RegisterVuforiaInitErrorCallback (TagInitFailed);
			ToggleARCameraSignal.AddListener (ToggleTagScan);
		}

		public override void OnRemove ()
		{
			VuforiaView.VuforiaBehaviour.UnregisterVuforiaInitErrorCallback (TagInitFailed);
			ToggleARCameraSignal.RemoveListener (ToggleTagScan);
		}

		void TagInitFailed(Vuforia.VuforiaUnity.InitError error)
		{
			Debug.LogWarning(error);
			TagInitFailedSignal.Dispatch ();
		}

		void ToggleTagScan(bool status)
		{
			VuforiaView.gameObject.SetActive (status);
		}
	}
}