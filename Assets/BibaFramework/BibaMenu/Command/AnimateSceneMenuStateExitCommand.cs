﻿using UnityEngine;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class AnimateSceneMenuStateExitCommand : Command 
    {
        [Inject]
		public MenuStateExitAnimationEndedSignal SceneMenuStateExitAnimationEndedSignal { get; set; }

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
                            
                            menuStateMediator.AnimateMenuExit();
                            SceneMenuStateExitAnimationEndedSignal.AddListener(ExitedAnimationCompleted);
                        }
                    }
                }
            }
        }

        void ExitedAnimationCompleted()
        {
			SceneMenuStateExitAnimationEndedSignal.RemoveListener(ExitedAnimationCompleted);
            Release();
        }
    }
}