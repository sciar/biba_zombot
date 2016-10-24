using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    public class URLButtonMediator : Mediator
    {
        [Inject]
        public URLButtonView ButtonView { get; set; }

        [Inject]
        public OpenURLSignal OpenURLSignal { get; set; }

        public override void OnRegister ()
        {
            ButtonView.ButtonClickedSignal.AddListener(OpenURL);
        }

        public override void OnRemove ()
        {
            ButtonView.ButtonClickedSignal.RemoveListener(OpenURL);
        }

        void OpenURL(string url)
        {
            OpenURLSignal.Dispatch(url);
        }
    }
}