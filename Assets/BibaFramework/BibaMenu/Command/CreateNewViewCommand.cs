using System.Collections;
using UnityEngine;
using BibaFramework.Utility;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class CreateNewViewCommand : Command 
    {
        [Inject]
        public PlayMenuLoadAnimationSignal PlayMenuLoadedAnimationSignal { get; set; }

        [Inject]
        public SetupMenuSignal SetupMenuSignal { get; set; } 

        [Inject]
        public BibaSceneModel BibaSceneModel { get; set; }
        
        private BibaMenuState menuState { get { return BibaSceneModel.NextMenuState; } }

        public override void Execute ()
        {
            if (menuState != null)
            {
                Retain();
                new Task(LoadLevelAsync(), true);
            }
        }

        IEnumerator LoadLevelAsync()
        {
            if (menuState.LoadingScreen)
            {
                PlayMenuLoadedAnimationSignal.Dispatch(true);
            }

            AsyncOperation asyncOp = Application.LoadLevelAdditiveAsync(menuState.GameScene.ToString());
            yield return asyncOp;

            if (menuState.LoadingScreen)
            {
                PlayMenuLoadedAnimationSignal.Dispatch(false);
            }

            LevelLoaded();
        }

        void LevelLoaded()
        {
            Release();
            SetupMenuSignal.Dispatch(menuState);;
        }
    }
}