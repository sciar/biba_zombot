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
            ProcessNewMenuState();
        }

        void PushMenuStateOnStack()
        {
            if (BibaSceneStack.Contains(NextMenuState))
			{
				return;
            }
            BibaSceneStack.Push(NextMenuState);
		}

        void ProcessNewMenuState()
        {
            if (NextMenuState is SceneMenuState)
            {
                LoadSceneMenuState();
            }
            else
            {
                EnableObjectMenuState();
            }
        }
		
        void EnableObjectMenuState()
        {
            ToggleObjectMenuStateSignal.Dispatch(NextMenuState as ObjectMenuState, true);
            LinkMenuStateWithGameObject();
        }

		void LoadSceneMenuState()
        {
			if (GameObject.Find(NextMenuState.SceneName) != null)
            {
                SetupSceneMenuState();
            } 
            else
            {
                LoadNewSceneMenuState();
            }
        }

        void LoadNewSceneMenuState()
        {
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
            SetupSceneMenuState();
        }

        void SetupSceneMenuState()
        {
            SetupSceneMenuStateSignal.Dispatch(NextMenuState as SceneMenuState);
            LinkMenuStateWithGameObject();
        }

        void LinkMenuStateWithGameObject()
        {
            var go = GameObject.Find(NextMenuState.SceneName);
            BibaSceneStack.LinkMenuStateWithGameObject(NextMenuState, go);
        }
	}
}