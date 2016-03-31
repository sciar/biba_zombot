using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine.UI;
using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    [RequireComponent(typeof(Button))]
    public class BibaButtonView : View
    {
        private Button _button;
        public Button Button { 
			get { 
				if(_button == null)
				{
					_button = (Button)GetComponent<Button>();
				}
				return _button; 
			} 
		}

        [HideInInspector]
        public string MenuStateTriggerString;
        
        [HideInInspector]
        public string SFXString = BibaSFX.None;

        public Signal<string> ButtonClickedSignal = new Signal<string>();

        public Signal<string> PlaySFXSignal = new Signal<string>();

        protected override void Start()
        {
			Button.onClick.AddListener (ButtonPressed);
        }

		protected override void OnDestroy()
		{
			Button.onClick.RemoveListener (ButtonPressed);
		}

		void ButtonPressed()
		{
			if(!string.IsNullOrEmpty(MenuStateTriggerString) && MenuStateTriggerString != MenuStateTrigger.None)
			{
				ButtonClickedSignal.Dispatch(MenuStateTriggerString);
			}
			
			if(SFXString != BibaSFX.None)
			{
				PlaySFXSignal.Dispatch(SFXString);
			}
		}
    }
}