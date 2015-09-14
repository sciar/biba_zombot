using UnityEngine;
using strange.extensions.command.impl;
using System.Collections;
using BibaFramework.Utility;

namespace BibaFramework.BibaMenu
{
    public class AnimateSceneEntryCommand : Command 
    {
        [Inject]
        public PlayMenuEntryAnimationSignal PlayMenuEntryAnimationSignal { get; set; }

        [Inject]
        public MenuEntryAnimationEndedSignal MenuEntryAnimationEndedSignal { get; set; }

        [Inject]
        public BibaSceneModel BibaSceneModel { get; set; }
        
        private BibaMenuState menuState { get { return BibaSceneModel.NextMenuState; } }

        public override void Execute ()
        {
            if (menuState != null && menuState.EntryAnimation)
            {
                Retain();

                Debug.Log("View Loading Animation Started: " + menuState.GameScene);
                PlayMenuEntryAnimationSignal.Dispatch();
                MenuEntryAnimationEndedSignal.AddListener(MenuEntryAnimationCompleted);
            }
        }

        void MenuEntryAnimationCompleted()
        {
            Debug.Log("View Loading Animation Completed: " + menuState.GameScene);

            MenuEntryAnimationEndedSignal.RemoveListener(MenuEntryAnimationCompleted);
            Release();
        }
    }
}