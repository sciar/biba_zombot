using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.Experimental.Director;

namespace BibaFramework.BibaMenu
{
    public class SetMenuStateConditionCommand : Command
    {
        [Inject]
        public string MenuStateCondition { get; set; }

        [Inject]
        public bool Status { get; set; }

        [Inject(BibaMenuConstants.BIBA_STATE_MACHINE)]
		public Animator StateMachine { get; set; }

        public override void Execute ()
        {
            StateMachine.SetBool(MenuStateCondition, Status);
        }
    }
}