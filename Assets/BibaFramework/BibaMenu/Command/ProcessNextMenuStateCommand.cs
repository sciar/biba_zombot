using strange.extensions.command.impl;
using UnityEngine;
using System;

namespace BibaFramework.BibaMenu
{
    public class ProcessNextMenuStateCommand : Command 
    {
        [Inject]
        public BaseMenuState NextMenuState { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        [Inject]
        public SwitchSceneMenuStateSignal SwitchSceneMenuStateSignal { get; set; }

        [Inject]
        public PushMenuStateSignal PushMenuStateSignal { get; set; }

        [Inject]
        public PopMenuStateSignal PopMenuStateSignal { get; set; }

        [Inject]
        public ReplaceMenuStateSignal ReplaceMenuStateSignal { get; set; }

        public override void Execute ()
        {
            if (NextMenuState.FullScreen)
            {
                ProcessFullScreenMenuState();
            }
            else
            {
                ProcessPopupMenuState();
            }
        }

        void ProcessFullScreenMenuState()
        {
            if (BibaSceneStack.Count == 0)
            {
                SwitchSceneMenuStateSignal.Dispatch(NextMenuState);
                return;
            }

            var lastMenuState = BibaSceneStack.Peek();
            if (lastMenuState == NextMenuState)
            {
                throw new Exception("Should not be able to transit to the same state.");
            }

            if (lastMenuState.FullScreen)
            {
                //Last MenuState is a Fullscreen SceneMenuState

                //Fullscreen SceneMenuState -> FullScreen SceneMenuState
                SwitchSceneMenuStateSignal.Dispatch(NextMenuState);
            }
            else
            {
                //Last MenuState is a Popup SceneMenuState or ObjectMenuState

                //Popup SceneMenuState or ObjectMenuState -> FullScreen SceneMenuState
                if(BibaSceneStack.Contains(NextMenuState))
                {
                    PopMenuStateSignal.Dispatch(NextMenuState);
                }
                else
                {
                    SwitchSceneMenuStateSignal.Dispatch(NextMenuState);
                }
            }

        }
        void ProcessPopupMenuState()
        {
            var lastMenuState = BibaSceneStack.Peek();

            //Fullscreen SceneMenuState -> Popup SceneMenuState or ObjectMenuState
            if (lastMenuState.FullScreen)
            {
                PushMenuStateSignal.Dispatch(NextMenuState);
            } 
            //Popup SceneMenuState or ObjectMenuState -> Popup SceneMenuState or ObjectMenuState 
            else
            {
                if(lastMenuState.Replace)
                {
                    ReplaceMenuStateSignal.Dispatch(NextMenuState);
                }
                else
                {
                    PushMenuStateSignal.Dispatch(NextMenuState);
                }
            }
        }
    }
}