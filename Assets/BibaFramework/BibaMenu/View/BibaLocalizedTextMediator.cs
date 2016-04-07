using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    public class BibaLocalizedTextMediator : Mediator
    {
        [Inject]
        public BibaLocalizedTextView BibaLocalizedTextView { get; set; }

        [Inject]
        public LocalizationService LocalizationService { get; set; }

		[Inject]
		public LanguageUpdatedSignal LanguageUpdatedSignal { get; set; } 

        public override void OnRegister ()
        {
			UpdateKey ();
			BibaLocalizedTextView.TextKeyUpdatedSignal.AddListener (UpdateKey);
			LanguageUpdatedSignal.AddListener (UpdateKey);
        }

		public override void OnRemove() {
			BibaLocalizedTextView.TextKeyUpdatedSignal.RemoveListener (UpdateKey);
			LanguageUpdatedSignal.RemoveListener (UpdateKey);
		}

		void UpdateKey() {
			BibaLocalizedTextView.Text.text = LocalizationService.GetText(BibaLocalizedTextView.Key);
		}
    }
}