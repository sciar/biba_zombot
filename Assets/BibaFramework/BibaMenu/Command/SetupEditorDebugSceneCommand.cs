using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.Experimental.Director;

namespace BibaFramework.BibaMenu
{
    public class SetupEditorDebugSceneCommand : Command
    {
        [Inject(BibaMenuConstants.BIBA_STATE_MACHINE)]
		public IAnimatorControllerPlayable StateMachine { get; set; }

        public override void Execute ()
        {
            #if UNITY_EDITOR
			((Animator)StateMachine).CrossFade(Application.loadedLevelName, 0);
            #endif
        }
    }
}