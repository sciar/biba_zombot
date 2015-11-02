using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class AnimateSceneMenuStateEntryCommand : Command 
    {
        [Inject]
		public MenuStateEntryAnimationEndedSignal SceneMenuStateEntryAnimationEndedSignal { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
            if (BibaSceneStack.Count > 0)
            {
                var menuState = BibaSceneStack.Peek();
                if (menuState != null && menuState is SceneMenuState)
                {
                    var menuStateGo = BibaSceneStack.GetTopGameObjectForTopMenuState();
                    if(menuStateGo != null)
                    {
                        var menuStateMediator = menuStateGo.GetComponent<SceneMenuStateMediator>();
                        if(menuStateMediator != null)
                        {
                            Retain();

                            menuStateMediator.AnimateMenuEntry();
                            SceneMenuStateEntryAnimationEndedSignal.AddListener(MenuEntryAnimationCompleted);
                        }
                    }
                }
            }
        }

        void MenuEntryAnimationCompleted()
        {
			SceneMenuStateEntryAnimationEndedSignal.RemoveListener(MenuEntryAnimationCompleted);
            Release();
        }
    }
}