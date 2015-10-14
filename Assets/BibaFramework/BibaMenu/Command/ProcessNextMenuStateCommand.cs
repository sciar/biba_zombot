using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class ProcessNextMenuStateCommand : Command 
    {
        [Inject]
        public BibaMenuState NextMenuState { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        [Inject]
        public LoadSceneBasedMenuStateSignal LoadFullSceneSignal { get; set; }

        [Inject]
        public PushSceneBasedMenuStateSignal PushPopupSceneSignal { get; set; }

        [Inject]
        public PopSceneBasedMenuStateSignal PopPopupSceneSignal { get; set; }

        [Inject]
        public ReplaceSceneBasedMenuStateSignal ReplacePopupSceneSignal { get; set; }

        public override void Execute ()
        {
            //The menustate is a 
            if (NextMenuState.MenuStateGameObject != null)
            {
                ProcessNextMenuStateAsGameObject();
            }
            else
            {
                ProcessNextMenuStateAsGameScene();
            }
        }

        void ProcessNextMenuStateAsGameObject()
        {
            if (BibaSceneStack.Count > 0)
            {
                var lastMenuState = BibaSceneStack.Peek();
                var nextMenuState = NextMenuState;


            }
        }
        
        void ProcessNextMenuStateAsGameScene()
        {
            if (BibaSceneStack.Count > 0)
            {
                var lastMenuState = BibaSceneStack.Peek();
                var nextMenuState = NextMenuState;
                
                if(lastMenuState.FullScreen && nextMenuState.FullScreen)
                {
                    LoadFullSceneSignal.Dispatch(NextMenuState);
                }
                else if(lastMenuState.FullScreen && nextMenuState.Popup)
                {
                    PushPopupSceneSignal.Dispatch(NextMenuState);
                }
                else if(lastMenuState.Popup && nextMenuState.Popup)
                {
                    if(nextMenuState.Replace)
                    {
                        ReplacePopupSceneSignal.Dispatch(NextMenuState);
                    }
                    else
                    {
                        PushPopupSceneSignal.Dispatch(NextMenuState);
                    }
                }
                else if(lastMenuState.Popup && nextMenuState.FullScreen)
                {
                    if(BibaSceneStack.Peek() == nextMenuState)
                    {
                        PopPopupSceneSignal.Dispatch(NextMenuState);
                    }
                    else
                    {
                        LoadFullSceneSignal.Dispatch(NextMenuState);
                    }
                }
            } 
            else
            {
                LoadFullSceneSignal.Dispatch(NextMenuState);
            }
        }
    }
}