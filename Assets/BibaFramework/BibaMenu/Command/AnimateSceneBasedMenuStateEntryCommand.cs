using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class AnimateSceneBasedMenuStateEntryCommand : Command 
    {
        [Inject]
        public PlaySceneBasedMenuStateEntryAnimationSignal PlaySceneBasedMenuStateEntryAnimationSignal { get; set; }

        [Inject]
        public SceneBasedMenuStateEntryAnimationEndedSignal SceneBasedMenuStateEntryAnimationEndedSignal { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
            var menuState = BibaSceneStack.Peek();
            if (menuState != null)
            {
                Retain();

                PlaySceneBasedMenuStateEntryAnimationSignal.Dispatch();
                SceneBasedMenuStateEntryAnimationEndedSignal.AddListener(MenuEntryAnimationCompleted);
            }
        }

        void MenuEntryAnimationCompleted()
        {
            SceneBasedMenuStateEntryAnimationEndedSignal.RemoveListener(MenuEntryAnimationCompleted);
            Release();
        }
    }
}