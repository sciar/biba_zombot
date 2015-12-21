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
        public Button Button { get { return _button; } }

        [HideInInspector]
        public string MenuStateTriggerString;
        
        [HideInInspector]
        public string SFXString = BibaSFX.None;

        public Signal<string> ButtonClickedSignal = new Signal<string>();

        protected override void Start()
        {
            _button = (Button)GetComponent<Button>();
            _button.onClick.AddListener(() => {
				
                if(!string.IsNullOrEmpty(MenuStateTriggerString) && MenuStateTriggerString != MenuStateTrigger.None)
                {
                    ButtonClickedSignal.Dispatch(MenuStateTriggerString);
                }
			});	
        }
    }
}