using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine.UI;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    [RequireComponent (typeof(Button))]
    public class BibaButtonView : View
    {
        private Button _button;

        public MenuStateTrigger MenuStateTrigger;
        public Signal<MenuStateTrigger> ButtonClickedSignal = new Signal<MenuStateTrigger>();
		public Signal<string> PlaySFXSignal = new Signal<string>();
		public string sfxNameOnClick;

        protected override void Start()
        {
            _button = (Button)GetComponent<Button>();
            _button.onClick.AddListener(() =>{
				ButtonClickedSignal.Dispatch(MenuStateTrigger);
				if (!string.IsNullOrEmpty(sfxNameOnClick)) {
					PlaySFXSignal.Dispatch(sfxNameOnClick);
				}
			});	
        }
    }
}