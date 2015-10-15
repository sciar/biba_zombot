using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class AnimateSceneMenuStateEntryCommand : Command 
    {
        [Inject]
		public PlaySceneMenuStateEntryAnimationSignal PlaySceneMenuStateEntryAnimationSignal { get; set; }

        [Inject]
		public SceneMenuStateEntryAnimationEndedSignal SceneMenuStateEntryAnimationEndedSignal { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
            var menuState = BibaSceneStack.Peek();
            if (menuState != null && menuState is SceneMenuState)
            {
                Retain();

				PlaySceneMenuStateEntryAnimationSignal.Dispatch();
				SceneMenuStateEntryAnimationEndedSignal.AddListener(MenuEntryAnimationCompleted);
            }
        }

        void MenuEntryAnimationCompleted()
        {
			SceneMenuStateEntryAnimationEndedSignal.RemoveListener(MenuEntryAnimationCompleted);
            Release();
        }
    }
}