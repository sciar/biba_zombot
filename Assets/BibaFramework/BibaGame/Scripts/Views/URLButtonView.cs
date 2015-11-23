using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    [RequireComponent (typeof(Button))]
    public class URLButtonView : View
    {
        private Button _button;
       
        public string URL = BibaGameConstants.BIBA_URL;
        public Signal<string> ButtonClickedSignal = new Signal<string>();

        protected override void Start()
        {
            gameObject.SetActive(Debug.isDebugBuild);

            _button = (Button)GetComponent<Button>();
            _button.onClick.AddListener(() =>{
                ButtonClickedSignal.Dispatch(URL);
			});	
        }
    }
}