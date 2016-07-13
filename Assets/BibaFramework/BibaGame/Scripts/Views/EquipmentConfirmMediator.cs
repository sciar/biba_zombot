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
		public BibaSession BibaSession { get; set; }

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
			EquipmentConfirmView.BibaButtonView.Button.interactable = BibaSession.SelectedEquipments.Count > 0;
			EquipmentConfirmView.BibaButtonView.MenuStateTriggerString = BibaSession.SelectedEquipments.Count >= 3 ? MenuStateTrigger.Yes : MenuStateTrigger.No;
		}
	}
}