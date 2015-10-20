using BibaFramework.BibaMenu;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectMediator : SceneMenuStateMediator
    {
        [Inject]
        public EquipmentSelectView EquipmentSelectView { get; set; }

        [Inject]
        public EquipmentSelectedSignal EquipmentSelectedSignal { get; set; }

        public override SceneMenuStateView View {
            get {
                return EquipmentSelectView;
            }
        }

        public override void RegisterSceneDependentSignals ()
        {
            EquipmentSelectView.ConfirmButton.onClick.AddListener(ConfirmEquipmentSelection);
        }

        public override void UnRegisterSceneDependentSignals ()
        {
            EquipmentSelectView.ConfirmButton.onClick.RemoveListener(ConfirmEquipmentSelection);
        }

        public override void SetupMenu (BaseMenuState menuState)
        {
        }

        void ConfirmEquipmentSelection()
        {
            EquipmentSelectedSignal.Dispatch(EquipmentSelectView.EquipmentSelectToggles.Where(est => est.IsOn).Select(est => est.BibaEquipmentType).ToList());
        }
    }
}