using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.Experimental.Director;

namespace BibaFramework.BibaMenu
{
    public class SetMenuStateTriggerCommand : Command
    {
        [Inject]
        public string MenuStateTrigger { get; set; }

        [Inject(BibaMenuConstants.BIBA_STATE_MACHINE)]
		public Animator StateMachine { get; set; }

        public override void Execute ()
        {
            StateMachine.SetTrigger(MenuStateTrigger);
        }
    }
}