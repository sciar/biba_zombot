using BibaFramework.BibaMenu;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectMediator : BaseBibaMediator
    {
        [Inject]
        public EquipmentSelectView EquipmentSelectView { get; set; }

        [Inject]
        public EquipmentSelectedSignal EquipmentSelectedSignal { get; set; }

        public override BaseBibaView View {
            get {
                return EquipmentSelectView;
            }
        }

        public override void RegisterSceneDependentSignals ()
        {
            EquipmentSelectView.ConfirmButton.onClick.AddListener(ConfirmEquipmentSelection);
        }

        public override void RemoveSceneDependentSignals ()
        {
            EquipmentSelectView.ConfirmButton.onClick.RemoveListener(ConfirmEquipmentSelection);
        }

        public override void SetupMenu (BibaMenuState menuState)
        {
        }

        void ConfirmEquipmentSelection()
        {
            EquipmentSelectedSignal.Dispatch(EquipmentSelectView.EquipmentSelectToggles.Where(est => est.IsOn).Select(est => est.BibaEquipmentType).ToList());
        }
    }
}