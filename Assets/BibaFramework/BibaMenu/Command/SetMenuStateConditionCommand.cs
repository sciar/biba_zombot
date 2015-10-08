using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class SetMenuStateConditionCommand : Command
    {
        [Inject]
        public MenuStateCondition MenuStateCondition { get; set; }

        [Inject]
        public bool Status { get; set; }

        [Inject(BibaMenuConstants.BIBA_STATE_MACHINE)]
        public Animator StateMachine { get; set; }

        public override void Execute ()
        {
            StateMachine.SetBool(MenuStateCondition.ToString(), Status);
        }
    }
}