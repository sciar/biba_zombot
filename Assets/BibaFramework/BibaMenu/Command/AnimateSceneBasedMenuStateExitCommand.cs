using UnityEngine;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class AnimateSceneBasedMenuStateExitCommand : Command 
    {
        [Inject]
        public PlaySceneBasedMenuStateExitAnimationSignal PlaySceneBasedMenuStateExitAnimationSignal { get; set; }

        [Inject]
        public SceneBasedMenuStateExitAnimationEndedSignal SceneBasedMenuStateExitAnimationEndedSignal { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
            if (BibaSceneStack.Count > 0)
            {
                var menuState = BibaSceneStack.Peek();
                if (menuState != null)
                {
                    Retain();
                    
                    PlaySceneBasedMenuStateExitAnimationSignal.Dispatch();
                    SceneBasedMenuStateExitAnimationEndedSignal.AddListener(ExitedAnimationCompleted);
                }
            }
        }

        void ExitedAnimationCompleted()
        {
            SceneBasedMenuStateExitAnimationEndedSignal.RemoveListener(ExitedAnimationCompleted);
            Release();
        }
    }
}