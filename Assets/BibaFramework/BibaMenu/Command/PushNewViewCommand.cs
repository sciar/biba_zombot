using System.Collections;
using UnityEngine;
using BibaFramework.Utility;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class PushNewViewCommand : Command 
    {
        [Inject]
        public PlayMenuLoadAnimationSignal PlayMenuLoadedAnimationSignal { get; set; }

        [Inject]
        public SetupMenuSignal SetupMenuSignal { get; set; } 

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        [Inject]
        public BibaMenuState BibaMenuState { get; set; }

        public override void Execute ()
        {
            Retain();

            PlayMenuLoadedAnimationSignal.Dispatch(true);
            BibaSceneStack.Push(BibaMenuState);
            new Task(LoadLevelAsync(), true);
        }

        IEnumerator LoadLevelAsync()
        {
            AsyncOperation asyncOp = Application.LoadLevelAdditiveAsync(BibaMenuState.GameScene.ToString());
            yield return asyncOp;

          //  yield return new WaitForSeconds(1.0f);
            LevelLoaded();
        }

        void LevelLoaded()
        {
            PlayMenuLoadedAnimationSignal.Dispatch(false);
            Release();
            SetupMenuSignal.Dispatch(BibaMenuState);;
        }
    }
}