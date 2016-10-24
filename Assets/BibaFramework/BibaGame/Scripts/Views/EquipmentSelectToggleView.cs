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
		public Toggle Toggle { 
			get {

				if(_toggle == null)
				{
					_toggle = GetComponent<Toggle>();
					_toggle.onValueChanged.AddListener(ToggleStatusChanged);
				}

				return _toggle;
			}
		}

        protected override void OnDestroy()
        {
            base.OnDestroy();
			Toggle.onValueChanged.RemoveListener(ToggleStatusChanged);
        }

        void ToggleStatusChanged(bool status)
        {
            EquipmentSelectToggleChangedSignal.Dispatch(status);
        }
    }
}