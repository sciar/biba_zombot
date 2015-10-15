using UnityEngine;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class RemoveAllMenuStatesCommand : Command 
    {
        [Inject]
        public BibaSceneStack BibaSceneMenuStateStack { get; set; }

		[Inject]
		public ToggleObjectMenuStateSignal ToggleObjectMenuStateSignal { get; set; }

        public override void Execute ()
        {
            RemoveAllGameViews();
        }

        void RemoveAllGameViews()
        {
            foreach (var menuState in BibaSceneMenuStateStack)
            {
				if(menuState is SceneMenuState)
				{
					DestroyGameView(menuState);
				}
				else
				{
					ToggleObjectMenuStateSignal.Dispatch(menuState as ObjectMenuState, false);
				}
            }

            BibaSceneMenuStateStack.Clear();
        }
       
        void DestroyGameView(BaseMenuState menuState)
        {
            var gameSceneGO = GameObject.Find(menuState.SceneName);
            GameObject.Destroy(gameSceneGO);
        }
    }
}