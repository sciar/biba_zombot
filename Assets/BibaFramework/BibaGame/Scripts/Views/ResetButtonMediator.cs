using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaGame
{
    public class ResetButtonMediator : Mediator
    {
        [Inject]
        public ResetButtonView ResetButtonView { get; set; }

        [Inject]
        public ResetGameModelSignal ResetGameModelSignal { get; set; }

        public override void OnRegister ()
        {
            ResetButtonView.ResetSignal.AddListener(ResetGameModel);
        }

        public override void OnRemove ()
        {
            ResetButtonView.ResetSignal.RemoveListener(ResetGameModel);
        }

        void ResetGameModel()
        {
            ResetGameModelSignal.Dispatch();
        }
    }
}