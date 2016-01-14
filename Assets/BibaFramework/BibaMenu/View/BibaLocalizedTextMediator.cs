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

        public override void OnRegister ()
        {
            BibaLocalizedTextView.Text.text = LocalizationService.GetText(BibaLocalizedTextView.Key);
        }
    }
}