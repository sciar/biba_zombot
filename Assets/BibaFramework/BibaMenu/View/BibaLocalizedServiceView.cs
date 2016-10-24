using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

public class BibaLocalizedServiceView : View {
	public LocalizationService localizationService;

	public string GetLocalizedText(string key) {
		return localizationService.GetText (key);
	}
}
