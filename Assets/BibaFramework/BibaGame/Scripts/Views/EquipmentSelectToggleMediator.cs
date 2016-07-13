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
        
		[Inject]
		public BibaSession BibaSession { get; set; }

        public override void OnRegister ()
        {
			Setup ();
            EquipmentSelectToggleView.EquipmentSelectToggleChangedSignal.AddListener(EquipmentSelectToggleChanged);
        }
        
        public override void OnRemove ()
        {
            EquipmentSelectToggleView.EquipmentSelectToggleChangedSignal.RemoveListener(EquipmentSelectToggleChanged);
        }
        
		void Setup()
		{
			var selected = BibaSession.SelectedEquipments.FindIndex (equip => equip.EquipmentType == EquipmentSelectToggleView.BibaEquipmentType) != -1;
			EquipmentSelectToggleView.Toggle.isOn = selected;
		}

        void EquipmentSelectToggleChanged(bool status)
        {
            EquipmentSelectedSignal.Dispatch(EquipmentSelectToggleView.BibaEquipmentType, status);
        }
    }
}