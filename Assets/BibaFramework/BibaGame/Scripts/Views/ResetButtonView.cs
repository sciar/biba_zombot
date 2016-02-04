using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace BibaFramework.BibaMenu
{
    [RequireComponent (typeof(Button))]
    public class ResetButtonView : View
    {
        private Button _button;
       
        public Signal ResetSignal = new Signal();

        protected override void Start()
        {
            _button = (Button)GetComponent<Button>();
            _button.onClick.AddListener(() =>{
                ResetSignal.Dispatch();
			});	
        }
    }
}