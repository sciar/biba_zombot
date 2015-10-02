using strange.extensions.mediation.impl;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectToggleMediator : Mediator
    {
        [Inject]
        public EquipmentSelectToggleView EquipmentSelectToggleView { get; set; }

        [Inject]
        public EquipmentSelectToggledSignal EquipmentSelectToggledSignal { get; set; }

        public override void OnRegister ()
        {
            EquipmentSelectToggleView.EquipmentButtonSelectedSignal.AddListener(EquipmentSelecteToggled);
        }

        public override void OnRemove ()
        {
            EquipmentSelectToggleView.EquipmentButtonSelectedSignal.RemoveListener(EquipmentSelecteToggled);
        }

        void EquipmentSelecteToggled(BibaEquipmentType equipmentType, bool status)
        {
            EquipmentSelectToggledSignal.Dispatch(equipmentType, status);
        }
    }
}