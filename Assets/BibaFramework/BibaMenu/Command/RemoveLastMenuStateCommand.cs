using UnityEngine;
using strange.extensions.command.impl;
using System.Collections;
using BibaFramework.Utility;

namespace BibaFramework.BibaMenu
{
    public class RemoveLastMenuStateCommand : Command 
    {
        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

		[Inject]
		public ToggleObjectMenuStateSignal ToggleObjectMenuStateSignal { get; set; }

        public override void Execute ()
        {
            if (BibaSceneStack.Count > 0)
            {
                RemoveLastGameView();
            }
        }

        void RemoveLastGameView()
        {
            var lastMenuStateGO = BibaSceneStack.GetTopGameObjectForTopMenuState();
            var lastMenuState = BibaSceneStack.Pop();

			if(lastMenuState is SceneMenuState)
			{
                Retain();
                new Task(WaitTilObjectDestroy(lastMenuStateGO), true);
			}
			else
			{
				ToggleObjectMenuStateSignal.Dispatch(lastMenuState as ObjectMenuState, false);
			}
        }

        IEnumerator WaitTilObjectDestroy(GameObject go)
        {
            GameObject.Destroy(go);
            while (go != null)
            {
                yield return null;
            }
            Release();
        }
    }
}