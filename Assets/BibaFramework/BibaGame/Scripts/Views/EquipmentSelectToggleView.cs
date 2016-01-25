using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace BibaFramework.BibaGame
{
    [RequireComponent(typeof(Toggle))]
    public class EquipmentSelectToggleView : View
    {
        public Signal<bool> EquipmentSelectToggleChangedSignal = new Signal<bool>();

        public BibaEquipmentType BibaEquipmentType;

        private Toggle _toggle;
        private Text _label;

        protected override void Start()
        {
            base.Start();
            _toggle = GetComponent<Toggle>();

            _label = GetComponentInChildren<Text>();
            _label.text = BibaEquipmentType.ToString();

            _toggle.onValueChanged.AddListener(ToggleStatusChanged);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _toggle.onValueChanged.RemoveListener(ToggleStatusChanged);
        }

        void ToggleStatusChanged(bool status)
        {
            EquipmentSelectToggleChangedSignal.Dispatch(status);
        }
    }
}