using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class SetupEditorGameSceneCommand : Command
    {
        [Inject(BibaMenuConstants.BIBA_STATE_MACHINE)]
        public Animator StateMachine { get; set; }

        public override void Execute ()
        {
            #if UNITY_EDITOR
            StateMachine.CrossFade(Application.loadedLevelName, 0);
            #endif
        }
    }
}