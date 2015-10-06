using BibaFramework.BibaMenu;
using System.Collections.Generic;
using UnityEngine.UI;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectView : BaseBibaView
    {
        public Button ConfirmButton;

        private List<EquipmentSelectToggleView> _equipmentSelectToggles;
        public List<EquipmentSelectToggleView> EquipmentSelectToggles {
            get {
                if(_equipmentSelectToggles == null)
                {
                    _equipmentSelectToggles = new List<EquipmentSelectToggleView>(gameObject.GetComponentsInChildren<EquipmentSelectToggleView>());
                }
                return _equipmentSelectToggles;
            }
        }
    }
}