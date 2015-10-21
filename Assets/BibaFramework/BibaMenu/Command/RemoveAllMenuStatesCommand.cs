using UnityEngine;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class RemoveAllMenuStatesCommand : Command 
    {
        [Inject]
        public BibaSceneStack BibaSceneMenuStateStack { get; set; }

        [Inject]
        public RemoveLastMenuStateSignal RemoveLastMenuStateSignal { get; set; }

        public override void Execute ()
        {
            RemoveAllGameViews();
        }

        void RemoveAllGameViews()
        {
            while (BibaSceneMenuStateStack.Count > 0)
            {
                RemoveLastMenuStateSignal.Dispatch();
            }
        }
    }
}