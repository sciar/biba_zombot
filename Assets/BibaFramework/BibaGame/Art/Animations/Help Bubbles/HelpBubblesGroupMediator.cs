using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

public class HelpBubblesGroupMediator : Mediator {

	[Inject]
	public HelpBubblesGroupView view { get; set; }

	[Inject]
	public BibaSystem BibaSystem { get; set; }

	public override void OnRegister ()
	{
		//base.OnRegister ();
		view.SetAnimatorBoolean (BibaSystem.HelpBubblesEnabled);
		view.bubblesEnabled = BibaSystem.HelpBubblesEnabled;
	}

	public override void OnRemove ()
	{
		//base.OnRemove ();
	}
}
