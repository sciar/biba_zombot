using strange.extensions.mediation.impl;

namespace BibaFramework.BibaMenu
{
    public class BibaButtonMediator : Mediator
    {
        [Inject]
        public BibaButtonView BibaButtonView { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        public override void OnRegister ()
        {
            BibaButtonView.ButtonClickedSignal.AddListener(SendMenuStateTrigger);
        }

        public override void OnRemove ()
        {
            BibaButtonView.ButtonClickedSignal.RemoveListener(SendMenuStateTrigger);
        }

        void SendMenuStateTrigger(MenuStateTrigger stateTrigger)
        {
            SetMenuStateTriggerSignal.Dispatch(stateTrigger);
        }
    }
}

