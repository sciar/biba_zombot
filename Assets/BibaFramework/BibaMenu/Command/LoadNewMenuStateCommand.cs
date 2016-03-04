using System.Collections;
using UnityEngine;
using BibaFramework.Utility;
using strange.extensions.command.impl;
using BibaFramework.BibaGame;
using BibaFramework.BibaNetwork;
using System;
using System.IO;

namespace BibaFramework.BibaMenu
{
    public class LoadNewMenuStateCommand : Command 
    {
        private const string FILE_SUFFIX = "file://";

        [Inject]
        public SetupSceneMenuStateSignal SetupSceneMenuStateSignal { get; set; } 

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        [Inject]
        public BaseMenuState NextMenuState { get; set; }

        [Inject]
        public ToggleObjectMenuStateSignal ToggleObjectMenuStateSignal { get; set; }

        [Inject]
        public SpecialSceneService SpecialSceneLoaderService { get; set; }

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
            AsyncOperation asyncOp;
            var specialSceneId = SpecialSceneLoaderService.GetNextSceneToShow(NextMenuState.SceneName);
            var persistedFilePath = BibaContentConstants.GetPersistedPath(specialSceneId + BibaContentConstants.UNITY3D_EXTENSION);
            if (!string.IsNullOrEmpty(specialSceneId) && File.Exists(persistedFilePath))
            {
                persistedFilePath = FILE_SUFFIX +  persistedFilePath;

                using(WWW www = WWW.LoadFromCacheOrDownload(persistedFilePath, 1))
                {
                    yield return www;

                    if(www.error != null)
                    {
                        throw new Exception("WWW download had an error:" + www.error);
                    }

                    www.assetBundle.LoadAsset(specialSceneId);
                  
                    asyncOp = Application.LoadLevelAdditiveAsync(specialSceneId);
                    yield return asyncOp;

                    www.assetBundle.Unload(false);
                    www.Dispose();
                }
            } 
            else
            {
                asyncOp = Application.LoadLevelAdditiveAsync(NextMenuState.SceneName);
                yield return asyncOp;
            }

            Resources.UnloadUnusedAssets();
            LevelLoaded();
        }

        void LevelLoaded()
        {
            Screen.orientation = ((SceneMenuState)NextMenuState).Orientation;
            SetupSceneMenuState();
            Release();
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