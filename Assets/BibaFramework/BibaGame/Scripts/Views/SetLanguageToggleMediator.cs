using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class SetLanguageToggleMediator : Mediator
	{
		[Inject]
		public SetLanguageToggleView SetLanguageToggleView { get; set; }

		[Inject]
		public SetLanguageOverwriteSignal SetLanguageOverwriteSignal { get; set; }

		[Inject]
		public GameModelUpdatedSignal GameModelUpdatedSignal { get; set; }

		[Inject]
		public BibaGameModel BibaGameModel { get; set; }

		public override void OnRegister ()
		{
			UpdateToggle ();
			SetLanguageToggleView.Text.text = SetLanguageToggleView.SystemLanguage.ToString ();

			SetLanguageToggleView.Toggle.onValueChanged.AddListener(SetLanguageOverwrite);
			GameModelUpdatedSignal.AddListener (UpdateToggle);
		}

		public override void OnRemove ()
		{
			SetLanguageToggleView.Toggle.onValueChanged.RemoveListener(SetLanguageOverwrite);
			GameModelUpdatedSignal.RemoveListener (UpdateToggle);
		}

		void SetLanguageOverwrite(bool status)
		{
			if (status) 
			{
				SetLanguageOverwriteSignal.Dispatch (SetLanguageToggleView.SystemLanguage);
			}
		}

		void UpdateToggle()
		{
			SetLanguageToggleView.Toggle.isOn = BibaGameModel.LanguageOverwrite == SetLanguageToggleView.SystemLanguage;
		}
	}
}