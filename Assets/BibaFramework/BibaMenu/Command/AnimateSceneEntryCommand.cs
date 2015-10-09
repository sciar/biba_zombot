using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class AnimateSceneEntryCommand : Command 
    {
        [Inject]
        public PlayMenuEntryAnimationSignal PlayMenuEntryAnimationSignal { get; set; }

        [Inject]
        public MenuEntryAnimationEndedSignal MenuEntryAnimationEndedSignal { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
            var menuState = BibaSceneStack.Peek();
            if (menuState != null)
            {
                Retain();

                PlayMenuEntryAnimationSignal.Dispatch();
                MenuEntryAnimationEndedSignal.AddListener(MenuEntryAnimationCompleted);
            }
        }

        void MenuEntryAnimationCompleted()
        {
            MenuEntryAnimationEndedSignal.RemoveListener(MenuEntryAnimationCompleted);
            Release();
        }
    }
}