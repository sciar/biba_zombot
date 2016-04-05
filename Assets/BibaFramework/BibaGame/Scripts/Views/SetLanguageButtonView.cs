using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace BibaFramework.BibaGame
{
	[RequireComponent (typeof(Button))]
	public class SetLanguageButtonView : View
	{
		public Text Text;
		public SystemLanguage SystemLanguage = SystemLanguage.Unknown;

		private Button _button;
		public Signal<SystemLanguage> ButtonPressedSignal = new Signal<SystemLanguage>();

		protected override void Start()
		{
			Text.text = SystemLanguage.ToString ();

			_button = (Button)GetComponent<Button>();
			_button.onClick.AddListener(() =>{
				ButtonPressedSignal.Dispatch(SystemLanguage);
			});	
		}
	}
}