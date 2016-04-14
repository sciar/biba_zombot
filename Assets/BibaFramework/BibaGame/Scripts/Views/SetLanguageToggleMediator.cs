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
		public LanguageUpdatedSignal LanguageUpdatedSignal { get; set; }

		[Inject]
		public BibaGameModel BibaGameModel { get; set; }

		public override void OnRegister ()
		{
			UpdateToggle ();

			SetLanguageToggleView.Toggle.onValueChanged.AddListener(SetLanguageOverwrite);
			LanguageUpdatedSignal.AddListener (UpdateToggle);
		}

		public override void OnRemove ()
		{
			SetLanguageToggleView.Toggle.onValueChanged.RemoveListener(SetLanguageOverwrite);
			LanguageUpdatedSignal.RemoveListener (UpdateToggle);
		}

		void SetLanguageOverwrite(bool status)
		{
			if (status) 
			{
				SetLanguageOverwriteSignal.Dispatch (SetLanguageToggleView.SystemLanguage);
			}

			UpdateToggle();
		}

		void UpdateToggle()
		{
			SetLanguageToggleView.Toggle.isOn = BibaGameModel.LanguageOverwrite == SetLanguageToggleView.SystemLanguage;
		}
	}
}