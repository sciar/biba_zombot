using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

public class BibaLocalizedServiceMediator : Mediator {

	[Inject]
	public LocalizationService localizationService { get; set; }

	[Inject]
	public BibaLocalizedServiceView view { get; set; }

	public override void OnRegister () {
		view.localizationService = localizationService;
	}

	public override void OnRemove() {
		
	}
}
