using UnityEngine;
using strange.extensions.command.impl;
using System.Collections;

namespace BibaFramework.BibaMenu
{
    public class UnloadGameSceneCommand : Command 
    {
        [Inject]
        public BibaMenuState BibaMenuState { get; set; }

        [Inject]
        public PlayMenuExitedAnimationSignal PlayMenuExitedAnimationSignal { get; set; }

        [Inject]
        public MenuExitAnimationEndedSignal MenuExitAnimationEndedSignal { get; set; }

        [Inject(BibaConstants.BIBA_STATE_MACHINE)]
        public Animator StateMachine { get; set; }

        public override void Execute ()
        {
            Debug.Log("View Unloading Started: " + BibaMenuState.GameScene.ToString());
            if (BibaMenuState.ExitAnimation)
            {
                Debug.Log("Unload Animation Started: " + BibaMenuState.GameScene.ToString());
                Retain();

                StateMachine.enabled = false;

                PlayMenuExitedAnimationSignal.Dispatch();
                MenuExitAnimationEndedSignal.AddListener(ExitedAnimationCompleted);
            } 
            else
            {
                DestroyGameView();
            }
        }

        void ExitedAnimationCompleted()
        {
            Debug.Log("Unload Animation Completed: " + BibaMenuState.GameScene.ToString());

            MenuExitAnimationEndedSignal.RemoveListener(ExitedAnimationCompleted);
  
            StateMachine.enabled = true;

            DestroyGameView();
            Release();
        }

        void DestroyGameView()
        {
            Debug.Log("View Unloading Completed: " + BibaMenuState.GameScene.ToString());

            var gameSceneGO = GameObject.Find(BibaMenuState.GameScene.ToString());
            GameObject.Destroy(gameSceneGO);
        }
    }
}