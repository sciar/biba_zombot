using System.Collections;
using UnityEngine;
using BibaFramework.Utility;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class LoadNewSceneMenuStateCommand : Command 
    {
        [Inject]
        public SetupSceneMenuStateSignal SetupSceneMenuStateSignal { get; set; } 

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        [Inject]
		public SceneMenuState SceneMenuState { get; set; }

        public override void Execute ()
        {
            PushMenuStateOnStack();
            LoadNewGameScene();
        }

        void PushMenuStateOnStack()
        {
			if (BibaSceneStack.Contains(SceneMenuState))
			{
				return;
            }
			BibaSceneStack.Push(SceneMenuState);
		}
		
		void LoadNewGameScene()
        {
			if(GameObject.Find(SceneMenuState.SceneName) != null)
            {
				SetupSceneMenuStateSignal.Dispatch(SceneMenuState);
				return;
            }
            
            Retain();
  
            new Task(LoadLevelAsync(), true);
        }

        IEnumerator LoadLevelAsync()
        {
			AsyncOperation asyncOp = Application.LoadLevelAdditiveAsync(SceneMenuState.SceneName);
            yield return asyncOp;

            LevelLoaded();
        }

        void LevelLoaded()
        {
            Release();
			SetupSceneMenuStateSignal.Dispatch(SceneMenuState);;
		}
	}
}