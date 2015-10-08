using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class SetMenuStateTriggerCommand : Command
    {
        [Inject]
        public MenuStateTrigger MenuStateTrigger { get; set; }

        [Inject(BibaMenuConstants.BIBA_STATE_MACHINE)]
        public Animator StateMachine { get; set; }

        public override void Execute ()
        {
            StateMachine.SetTrigger(MenuStateTrigger.ToString());
        }
    }
}