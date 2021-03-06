﻿using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

public class HelpBubblesGroupMediator : Mediator {

	[Inject]
	public HelpBubblesGroupView view { get; set; }

	[Inject]
	public BibaDevice BibaDevice { get; set; }

	public override void OnRegister ()
	{
		//base.OnRegister ();
		view.SetAnimatorBoolean (BibaDevice.HelpBubblesEnabled);
		view.bubblesEnabled = BibaDevice.HelpBubblesEnabled;
	}

	public override void OnRemove ()
	{
		//base.OnRemove ();
	}
}
