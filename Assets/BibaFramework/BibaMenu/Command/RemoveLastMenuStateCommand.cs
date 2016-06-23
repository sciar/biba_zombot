using System.Collections;
using BibaFramework.Utility;
using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                new Task(WaitTilObjectDestroy(lastMenuState, lastMenuStateGO), true);
			}
			else
			{
				ToggleObjectMenuStateSignal.Dispatch(lastMenuState as ObjectMenuState, false);
			}
        }

		IEnumerator WaitTilObjectDestroy(BaseMenuState menuState, GameObject menuStateGO)
        {
            GameObject.Destroy(menuStateGO);
            while (menuStateGO != null)
            {
                yield return null;
            }
			SceneManager.UnloadScene (menuState.SceneName);
            Resources.UnloadUnusedAssets();
            Release();
        }
    }
}