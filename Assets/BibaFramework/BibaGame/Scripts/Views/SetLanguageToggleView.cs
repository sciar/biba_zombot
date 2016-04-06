using UnityEngine.UI;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaGame
{
	[RequireComponent (typeof(Toggle))]
	public class SetLanguageToggleView : View
	{
		public Text Text;
		public Toggle Toggle;
		public SystemLanguage SystemLanguage = SystemLanguage.Unknown;
	}
}