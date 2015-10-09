using UnityEngine;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class AnimateSceneExitCommand : Command 
    {
        [Inject]
        public PlayMenuExitedAnimationSignal PlayMenuExitedAnimationSignal { get; set; }

        [Inject]
        public MenuExitAnimationEndedSignal MenuExitAnimationEndedSignal { get; set; }

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
                    
                    PlayMenuExitedAnimationSignal.Dispatch();
                    MenuExitAnimationEndedSignal.AddListener(ExitedAnimationCompleted);
                }
            }
        }

        void ExitedAnimationCompleted()
        {
            MenuExitAnimationEndedSignal.RemoveListener(ExitedAnimationCompleted);
            Release();
        }
    }
}