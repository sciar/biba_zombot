using UnityEngine;
using UnityEngine.UI;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
	public class EnableObjectMenuStateCommand : Command 
    {
        [Inject]
		public ToggleObjectMenuStateSignal ToggleObjectMenuStateSignal { get; set; }

		[Inject]
		public ObjectMenuState ObjectMenuState { get; set; }

		[Inject]
		public BibaSceneStack BibaSceneStack { get; set; } 

        public override void Execute ()
        {
			BibaSceneStack.Push(ObjectMenuState);
			ToggleObjectMenuStateSignal.Dispatch(ObjectMenuState, true);
        }
    }
}