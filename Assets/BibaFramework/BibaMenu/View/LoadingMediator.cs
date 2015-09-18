using strange.extensions.mediation.impl;

namespace BibaFramework.BibaMenu
{
    public class LoadingMediator : Mediator
    {
        [Inject]
        public LoadingView View { get; set; }

        [Inject]
        public PlayMenuLoadAnimationSignal PlayMenuLoadAnimationSignal { get; set; }

        public override void OnRegister ()
        {
            PlayMenuLoadAnimationSignal.AddListener(ShowLoading);
        }

        public override void OnRemove ()
        {
            PlayMenuLoadAnimationSignal.RemoveListener(ShowLoading);
        }

        void ShowLoading(bool status)
        {
            View.Enable(status); 
        }
    }
}