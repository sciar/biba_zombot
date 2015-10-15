using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class ProcessNextMenuStateCommand : Command 
    {
        [Inject]
        public BaseMenuState NextMenuState { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        [Inject]
        public LoadSceneMenuStateSignal LoadFullSceneSignal { get; set; }

        [Inject]
        public PushSceneMenuStateSignal PushPopupSceneSignal { get; set; }

        [Inject]
        public PopSceneMenuStateSignal PopPopupSceneSignal { get; set; }

        [Inject]
        public ReplaceSceneMenuStateSignal ReplacePopupSceneSignal { get; set; }

		[Inject]
		public PushObjectMenuStateSignal EnableObjectMenuStateSignal { get; set; }

		[Inject]
		public ReplaceObjectMenuStateSignal ReplaceObjectMenuStateSignal { get; set; }

        public override void Execute ()
        {
			Debug.Log (BibaSceneStack);

            if (NextMenuState is ObjectMenuState)
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
			if(NextMenuState.Replace)
			{
				ReplaceObjectMenuStateSignal.Dispatch(NextMenuState as ObjectMenuState);
			}
			else
			{
				EnableObjectMenuStateSignal.Dispatch(NextMenuState as ObjectMenuState);
			}
        }
        
        void ProcessNextMenuStateAsGameScene()
        {
            if (BibaSceneStack.Count > 0)
            {
				if(BibaSceneStack.Peek() is ObjectMenuState)
				{;
					var nextMenuState = NextMenuState as SceneMenuState;
					if(nextMenuState.Popup)
					{
						if(nextMenuState.Replace)
						{
							ReplacePopupSceneSignal.Dispatch(nextMenuState);
						}
						else
						{
							PushPopupSceneSignal.Dispatch(nextMenuState);
						}
					}
					else if(nextMenuState.FullScreen)
					{
						if(BibaSceneStack.Peek() == nextMenuState)
						{
							PopPopupSceneSignal.Dispatch(nextMenuState);
						}
						else
						{
							LoadFullSceneSignal.Dispatch(nextMenuState);
						}
					}
				}
				else
				{
					var lastMenuState = BibaSceneStack.Peek() as SceneMenuState;
					var nextMenuState = NextMenuState as SceneMenuState;
					
					if(lastMenuState.FullScreen && nextMenuState.FullScreen)
					{
						LoadFullSceneSignal.Dispatch(nextMenuState);
					}
					else if(lastMenuState.Popup && nextMenuState.FullScreen)
					{
						if(BibaSceneStack.Peek() == nextMenuState)
						{
							PopPopupSceneSignal.Dispatch(nextMenuState);
						}
						else
						{
							LoadFullSceneSignal.Dispatch(nextMenuState);
						}
					}
					else if(lastMenuState.FullScreen && nextMenuState.Popup)
					{
						PushPopupSceneSignal.Dispatch(nextMenuState);
					}
					else if(lastMenuState.Popup && nextMenuState.Popup)
					{
						if(nextMenuState.Replace)
						{
							ReplacePopupSceneSignal.Dispatch(nextMenuState);
						}
						else
						{
							PushPopupSceneSignal.Dispatch(nextMenuState);
						}
					}
				}
            } 
            else
            {
				LoadFullSceneSignal.Dispatch(NextMenuState as SceneMenuState);
			}
		}
    }
}