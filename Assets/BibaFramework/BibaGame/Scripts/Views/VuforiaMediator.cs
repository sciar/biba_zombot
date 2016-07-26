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

		public override void OnRegister ()
		{
			VuforiaView.VuforiaBehaviour.RegisterVuforiaInitErrorCallback (TagInitFailed);
		}

		public override void OnRemove ()
		{
			VuforiaView.VuforiaBehaviour.UnregisterVuforiaInitErrorCallback (TagInitFailed);
		}

		void TagInitFailed(Vuforia.VuforiaUnity.InitError error)
		{
			Debug.LogWarning(error);
			TagInitFailedSignal.Dispatch ();
		}
	}
}