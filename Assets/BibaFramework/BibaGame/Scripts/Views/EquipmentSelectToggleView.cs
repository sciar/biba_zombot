using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaGame
{
    [RequireComponent(typeof(Toggle))]
    public class EquipmentSelectToggleView : View
    {
        public BibaEquipmentType BibaEquipmentType;

        private Toggle _toggle;
        private Text _label;

        protected override void Start()
        {
            base.Start();
            _toggle = GetComponent<Toggle>();

            _label = GetComponentInChildren<Text>();
            _label.text = BibaEquipmentType.ToString();
        }

        public bool IsOn {
            get {
                return _toggle.isOn;
            }
        }
    }
}