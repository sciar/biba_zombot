using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    public class BibaButtonMediator : Mediator
    {
        [Inject]
        public BibaButtonView BibaButtonView { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

		[Inject]
		public AudioServices audioServices { get; set; }

        [Inject]
        public PlayBibaSFXSignal PlayBibaSFXSignal { get; set; }

        public override void OnRegister ()
        {
            BibaButtonView.ButtonClickedSignal.AddListener(SendMenuStateTrigger);
        }

        public override void OnRemove ()
        {
            BibaButtonView.ButtonClickedSignal.RemoveListener(SendMenuStateTrigger);
        }

        void SendMenuStateTrigger(string stateTrigger)
        {
            SetMenuStateTriggerSignal.Dispatch(stateTrigger);

            if(BibaButtonView.SFXString != BibaSFX.None)
            {
                PlayBibaSFXSignal.Dispatch(BibaButtonView.SFXString);
            }
        }
    }
}