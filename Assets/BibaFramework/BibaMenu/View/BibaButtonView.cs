using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine.UI;
using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    [ExecuteInEditMode]
    [RequireComponent (typeof(Button))]
    public class BibaButtonView : View
    {
        private Button _button;
        public Button Button { get { return _button; } }

        [HideInInspector]
        public string MenuStateTriggerString;
        public MenuStateTrigger MenuStateTrigger;
    
        [HideInInspector]
        public string BibaSFXString;
        public BibaSFX BibaSFX;

        public Signal<MenuStateTrigger> ButtonClickedSignal = new Signal<MenuStateTrigger>();
        public Signal<BibaSFX> PlaySFXSignal = new Signal<BibaSFX>();

        protected override void Start()
        {
            _button = (Button)GetComponent<Button>();
            _button.onClick.AddListener(() => {
				
                if(MenuStateTrigger != MenuStateTrigger.None)
                {
                    ButtonClickedSignal.Dispatch(MenuStateTrigger);
                }

                if(BibaSFX != BibaSFX.none)
                {
                    PlaySFXSignal.Dispatch(BibaSFX);
                }
			});	
        }
    }
}