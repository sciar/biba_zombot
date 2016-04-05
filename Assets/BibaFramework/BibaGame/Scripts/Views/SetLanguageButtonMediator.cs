using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class SetLanguageButtonMediator : Mediator
	{
		[Inject]
		public SetLanguageButtonView SetLanguageButtonView { get; set; }

		[Inject]
		public SetLanguageOverwriteSignal SetLanguageOverwriteSignal { get; set; }

		public override void OnRegister ()
		{
			SetLanguageButtonView.ButtonPressedSignal.AddListener(SetLanguageOverwrite);
		}

		public override void OnRemove ()
		{
			SetLanguageButtonView.ButtonPressedSignal.RemoveListener(SetLanguageOverwrite);
		}

		void SetLanguageOverwrite(SystemLanguage language)
		{
			SetLanguageOverwriteSignal.Dispatch (language);
		}
	}
}