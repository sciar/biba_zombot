using System;
using BibaFramework.BibaMenu;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaGame
{
	public class EquipmentConfirmMediator : Mediator
	{
		[Inject]
		public EquipmentConfirmView EquipmentConfirmView { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public SessionUpdatedSignal SessionUpdatedSignal { get; set; }

		public override void OnRegister ()
		{
			SessionUpdatedSignal.AddListener(UpdateNextButton);
			UpdateNextButton ();
		}

		public override void OnRemove ()
		{
			SessionUpdatedSignal.RemoveListener(UpdateNextButton);
		}

		void UpdateNextButton()
		{
			EquipmentConfirmView.BibaButtonView.Button.interactable = BibaDeviceSession.SelectedEquipments.Count > 0;
			EquipmentConfirmView.BibaButtonView.MenuStateTriggerString = BibaDeviceSession.SelectedEquipments.Count >= 3 ? MenuStateTrigger.Yes : MenuStateTrigger.No;
		}
	}
}