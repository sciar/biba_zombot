using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine.UI;
using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    [RequireComponent (typeof(Button))]
    public class BibaButtonView : View
    {
        private Button _button;

        public MenuStateTrigger MenuStateTrigger;
        public Signal<MenuStateTrigger> ButtonClickedSignal = new Signal<MenuStateTrigger>();
        public Signal<BibaSFX> PlaySFXSignal = new Signal<BibaSFX>();
		public BibaSFX sfxNameOnClick;

        protected override void Start()
        {
            _button = (Button)GetComponent<Button>();
            _button.onClick.AddListener(() =>{
				
                ButtonClickedSignal.Dispatch(MenuStateTrigger);
                PlaySFXSignal.Dispatch(sfxNameOnClick);
			});	
        }
    }
}