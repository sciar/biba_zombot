using BibaFramework.BibaMenu;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectMediator : SceneMenuStateMediator
    {
        [Inject]
        public EquipmentSelectView EquipmentSelectView { get; set; }

        [Inject]
        public GameModelUpdatedSignal GameModelUpdatedSignal { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        public override SceneMenuStateView View {
            get {
                return EquipmentSelectView;
            }
        }

        public override void RegisterSceneDependentSignals ()
        {
            GameModelUpdatedSignal.AddListener(OnGameModelUpdated);
        }

        public override void UnRegisterSceneDependentSignals ()
        {
            GameModelUpdatedSignal.RemoveListener(OnGameModelUpdated);
        }

        public override void SetupMenu (BaseMenuState menuState)
        {
        }

        void OnGameModelUpdated()
        {
            if (EquipmentSelectView.ConfirmButton != null)
            {
                EquipmentSelectView.ConfirmButton.interactable = BibaGameModel.SelectedEquipments.Count > 0;
            }
        }
    }
}