using System.Collections;
using UnityEngine;
using BibaFramework.Utility;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class LoadNewMenuStateCommand : Command 
    {
        [Inject]
        public SetupSceneMenuStateSignal SetupSceneMenuStateSignal { get; set; } 

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        [Inject]
        public BaseMenuState NextMenuState { get; set; }

        [Inject]
        public ToggleObjectMenuStateSignal ToggleObjectMenuStateSignal { get; set; }

        public override void Execute ()
        {
            PushMenuStateOnStack();
            LoadNewMenuState();
        }

        void PushMenuStateOnStack()
        {
            if (BibaSceneStack.Contains(NextMenuState))
			{
				return;
            }
            BibaSceneStack.Push(NextMenuState);
		}

        void LoadNewMenuState()
        {
            if (NextMenuState is SceneMenuState)
            {
                LoadNewGameSceneMenuState();
            }
            else
            {
                EnableNewObjectMenuState();
            }
        }
		
        void EnableNewObjectMenuState()
        {
            ToggleObjectMenuStateSignal.Dispatch(NextMenuState as ObjectMenuState, true);
        }

		void LoadNewGameSceneMenuState()
        {
			if(GameObject.Find(NextMenuState.SceneName) != null)
            {
				SetupSceneMenuStateSignal.Dispatch(NextMenuState as SceneMenuState);
				return;
            }
            
            Retain();
  
            new Task(LoadLevelAsync(), true);
        }

        IEnumerator LoadLevelAsync()
        {
			AsyncOperation asyncOp = Application.LoadLevelAdditiveAsync(NextMenuState.SceneName);
            yield return asyncOp;

            LevelLoaded();
        }

        void LevelLoaded()
        {
            Release();
			SetupSceneMenuStateSignal.Dispatch(NextMenuState as SceneMenuState);
		}
	}
}