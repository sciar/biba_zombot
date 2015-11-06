using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

public class HelpBubblesGroupMediator : Mediator {

	[Inject]
	public HelpBubblesGroupView view { get; set; }

	[Inject]
	public BibaGameModel model { get; set; }

	public override void OnRegister ()
	{
		//base.OnRegister ();
		view.SetAnimatorBoolean (model.HelpBubblesEnabled);
		view.bubblesEnabled = model.HelpBubblesEnabled;
	}

	public override void OnRemove ()
	{
		//base.OnRemove ();
	}
}
