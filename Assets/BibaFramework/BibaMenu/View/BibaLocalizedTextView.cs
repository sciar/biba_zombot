using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaMenu
{
    [RequireComponent(typeof(Text))]
    public class BibaLocalizedTextView : View
    {
        private Text _text;
        public Text Text { 
            get {
                if(_text == null)
                {
                    _text = (Text)GetComponent<Text>();
                }
                return _text;
            }
        }

        [HideInInspector]
        public string Key;
    }
}