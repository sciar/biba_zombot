using UnityEngine;
using strange.extensions.command.impl;
using System.Collections;
using BibaFramework.Utility;

namespace BibaFramework.BibaMenu
{
    public class RemoveAllMenuStatesCommand : Command 
    {
        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }
        
        [Inject]
        public ToggleObjectMenuStateSignal ToggleObjectMenuStateSignal { get; set; }
        
        public override void Execute ()
        {
            if (BibaSceneStack.Count > 0)
            {
                Retain();
                new Task(WaitToDestoryAllObjects(), true);
            }
        }

        IEnumerator WaitToDestoryAllObjects()
        {
            while (BibaSceneStack.Count > 0)
            {
                var lastMenuStateGO = BibaSceneStack.GetTopGameObjectForTopMenuState();
                var lastMenuState = BibaSceneStack.Pop();
    
                if(lastMenuState is SceneMenuState)
                {
                    GameObject.Destroy(lastMenuStateGO);
                    while(lastMenuStateGO != null)
                    {
                        yield return null;
                    }
                }
                else
                {
                    ToggleObjectMenuStateSignal.Dispatch(lastMenuState as ObjectMenuState, false);
                }
            }

            Release();
        }
    }
}