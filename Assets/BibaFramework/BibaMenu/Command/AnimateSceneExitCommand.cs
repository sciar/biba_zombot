using UnityEngine;
using strange.extensions.command.impl;
using System.Collections;

namespace BibaFramework.BibaMenu
{
    public class AnimateSceneExitCommand : Command 
    {
        [Inject]
        public PlayMenuExitedAnimationSignal PlayMenuExitedAnimationSignal { get; set; }

        [Inject]
        public MenuExitAnimationEndedSignal MenuExitAnimationEndedSignal { get; set; }

        [Inject]
        public BibaSceneModel BibaSceneModel { get; set; }

        private BibaMenuState menuState { get { return BibaSceneModel.LastMenuState; } }

        public override void Execute ()
        {
            if (menuState != null && menuState.ExitAnimation)
            {
                Debug.Log("View Unloading Animation Started: " + menuState.GameScene.ToString());
                Retain();
                
                PlayMenuExitedAnimationSignal.Dispatch();
                MenuExitAnimationEndedSignal.AddListener(ExitedAnimationCompleted);
            }
        }

        void ExitedAnimationCompleted()
        {
            Debug.Log("View Unloading Animation Completed: " + menuState.GameScene.ToString());

            MenuExitAnimationEndedSignal.RemoveListener(ExitedAnimationCompleted);
            Release();
        }
    }
}