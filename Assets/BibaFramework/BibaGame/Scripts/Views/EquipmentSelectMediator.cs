using BibaFramework.BibaMenu;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectMediator : SceneMenuStateMediator
    {
        [Inject]
        public EquipmentSelectView EquipmentSelectView { get; set; }

        [Inject]
		public BibaSession BibaSession { get; set; }

		[Inject]
		public SessionUpdatedSignal SessionUpdatedSignal { get; set; }

        public override SceneMenuStateView View {
            get {
                return EquipmentSelectView;
            }
        }

        public override void RegisterSceneDependentSignals ()
        {
			UpdateNextButton ();
			SessionUpdatedSignal.AddListener(UpdateNextButton);
        }

        public override void UnRegisterSceneDependentSignals ()
        {
			SessionUpdatedSignal.RemoveListener(UpdateNextButton);
        }

        public override void SetupSceneDependentMenu ()
        {
        }

		void UpdateNextButton()
        {
            if (EquipmentSelectView != null && EquipmentSelectView.ConfirmButton.Button != null)
            {
				EquipmentSelectView.ConfirmButton.Button.interactable = BibaSession.SelectedEquipments.Count > 0;
				EquipmentSelectView.ConfirmButton.MenuStateTriggerString = BibaSession.SelectedEquipments.Count >= 3 ? MenuStateTrigger.Yes : MenuStateTrigger.No;
            }
        }
    }
}