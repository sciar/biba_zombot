using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace BibaFramework.BibaMenu
{
    [RequireComponent(typeof(Text))]
    public class BibaLocalizedTextView : View
    {
        private Text _text;
        public Text Text { 
            get {
				if (_text == null) 
				{
					_text = GetComponent<Text>();
				}
                return _text;
            }
        }

        [HideInInspector]
        public string Key {
			get {
				return _Key;
			}
			set {
				TextKeyUpdatedSignal.Dispatch();
				_Key = value;
			}
		}

		[SerializeField]
		private string _Key;
		public Signal TextKeyUpdatedSignal = new Signal();
    }
}