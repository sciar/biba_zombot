using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectToggleMediator : Mediator
    {
        [Inject]
        public EquipmentSelectToggleView EquipmentSelectToggleView { get; set; }
        
        [Inject]
        public EquipmentSelectedSignal EquipmentSelectedSignal { get; set; }
        
        public override void OnRegister ()
        {
            EquipmentSelectToggleView.EquipmentSelectToggleChangedSignal.AddListener(EquipmentSelectToggleChanged);
        }
        
        public override void OnRemove ()
        {
            EquipmentSelectToggleView.EquipmentSelectToggleChangedSignal.RemoveListener(EquipmentSelectToggleChanged);
        }
        
        void EquipmentSelectToggleChanged(bool status)
        {
            EquipmentSelectedSignal.Dispatch(EquipmentSelectToggleView.BibaEquipmentType, status);
        }
    }
}