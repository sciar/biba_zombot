using System.Collections;
using UnityEngine;
using BibaFramework.Utility;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class PushNewViewCommand : Command 
    {
        [Inject]
        public SetupMenuSignal SetupMenuSignal { get; set; } 

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        [Inject]
        public BibaMenuState BibaMenuState { get; set; }

        public override void Execute ()
        {
            PushMenuStateOnStack();
            LoadNewGameScene();
        }

        void PushMenuStateOnStack()
        {
            if (BibaSceneStack.Contains(BibaMenuState))
            {
                return;
            }
            BibaSceneStack.Push(BibaMenuState);
        }

        void LoadNewGameScene()
        {
            if(GameObject.Find(BibaMenuState.GameScene.ToString()) != null)
            {
                SetupMenuSignal.Dispatch(BibaMenuState);
                return;
            }
            
            Retain();
            
            new Task(LoadLevelAsync(), true);
        }

        IEnumerator LoadLevelAsync()
        {
            AsyncOperation asyncOp = Application.LoadLevelAdditiveAsync(BibaMenuState.GameScene.ToString());
            yield return asyncOp;

            LevelLoaded();
        }

        void LevelLoaded()
        {
            Release();
            SetupMenuSignal.Dispatch(BibaMenuState);;
        }
    }
}