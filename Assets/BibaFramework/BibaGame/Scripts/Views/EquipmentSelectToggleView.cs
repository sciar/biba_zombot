using UnityEngine.UI;
using strange.extensions.mediation.api;
using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    [RequireComponent(typeof(Toggle))]
    public class EquipmentSelectToggleView : View
    {
        private Toggle _toggle;
        private Text _label;
        public BibaEquipmentType BibaEquipmentType;
        public Signal<BibaEquipmentType, bool> EquipmentButtonSelectedSignal = new Signal<BibaEquipmentType, bool>();

        protected override void Start()
        {
            base.Start();
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(ToggleStatusChanged);

            _label = GetComponentInChildren<Text>();
            _label.text = BibaEquipmentType.ToString();
        }
        
        protected override void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(ToggleStatusChanged);
        }

        void ToggleStatusChanged(bool status)
        {
            EquipmentButtonSelectedSignal.Dispatch(BibaEquipmentType, status);
        }
    }
}