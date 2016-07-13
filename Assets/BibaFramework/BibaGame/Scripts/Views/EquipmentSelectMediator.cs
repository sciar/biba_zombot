using BibaFramework.BibaMenu;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectMediator : SceneMenuStateMediator
    {
        [Inject]
        public EquipmentSelectView EquipmentSelectView { get; set; }

        [Inject]
        public AccountUpdatedSignal GameModelUpdatedSignal { get; set; }

        [Inject]
		public BibaSession BibaSession { get; set; }

        public override SceneMenuStateView View {
            get {
                return EquipmentSelectView;
            }
        }

        public override void RegisterSceneDependentSignals ()
        {
			OnGameModelUpdated ();
            GameModelUpdatedSignal.AddListener(OnGameModelUpdated);
        }

        public override void UnRegisterSceneDependentSignals ()
        {
            GameModelUpdatedSignal.RemoveListener(OnGameModelUpdated);
        }

        public override void SetupSceneDependentMenu ()
        {
        }

        void OnGameModelUpdated()
        {
            if (EquipmentSelectView != null && EquipmentSelectView.ConfirmButton.Button != null)
            {
				EquipmentSelectView.ConfirmButton.Button.interactable = BibaSession.SelectedEquipments.Count > 0;
				EquipmentSelectView.ConfirmButton.MenuStateTriggerString = BibaSession.SelectedEquipments.Count >= 3 ? MenuStateTrigger.Yes : MenuStateTrigger.No;
            }
        }
    }
}