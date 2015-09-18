using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class ProcessNextMenuStateCommand : Command 
    {
        [Inject]
        public BibaMenuState BibaMenuState { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        [Inject]
        public LoadFullSceneSignal LoadFullSceneSignal { get; set; }

        [Inject]
        public PushPopupSceneSignal PushPopupSceneSignal { get; set; }

        [Inject]
        public PopPopupSceneSignal PopPopupSceneSignal { get; set; }

        [Inject]
        public ReplacePopupSceneSignal ReplacePopupSceneSignal { get; set; }

        public override void Execute ()
        {
            if (BibaSceneStack.Count > 0)
            {
                var lastMenuState = BibaSceneStack.Peek();
                var nextMenuState = BibaMenuState;

                if(lastMenuState.FullScreen && nextMenuState.FullScreen)
                {
                    LoadFullSceneSignal.Dispatch(BibaMenuState);
                }
                else if(lastMenuState.FullScreen && nextMenuState.Popup)
                {
                    PushPopupSceneSignal.Dispatch(BibaMenuState);
                }
                else if(lastMenuState.Popup && nextMenuState.Popup)
                {
                    if(nextMenuState.Replace)
                    {
                        ReplacePopupSceneSignal.Dispatch(BibaMenuState);
                    }
                    else
                    {
                        PushPopupSceneSignal.Dispatch(BibaMenuState);
                    }
                }
                else if(lastMenuState.Popup && nextMenuState.FullScreen)
                {
                    if(BibaSceneStack.Peek() == nextMenuState)
                    {
                        PopPopupSceneSignal.Dispatch(BibaMenuState);
                    }
                    else
                    {
                        LoadFullSceneSignal.Dispatch(BibaMenuState);
                    }
                }
            } 
            else
            {
                LoadFullSceneSignal.Dispatch(BibaMenuState);
            }
        }
    }
}