using UnityEngine;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class RemoveLastMenuStateCommand : Command 
    {
        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

		[Inject]
		public ToggleObjectMenuStateSignal ToggleObjectMenuStateSignal { get; set; }
        public override void Execute ()
        {
            if (BibaSceneStack.Count > 0)
            {
                RemoveLastGameView();
            }
        }

        void RemoveLastGameView()
        {
			var lastMenuState = BibaSceneStack.Pop();
			if(lastMenuState is SceneMenuState)
			{
				DestroyGameView(lastMenuState);
			}
			else
			{
				ToggleObjectMenuStateSignal.Dispatch(lastMenuState as ObjectMenuState, false);
			}
        }

        void DestroyGameView(BaseMenuState menuState)
        {
            var gameSceneGO = GameObject.Find(menuState.SceneName);
            GameObject.Destroy(gameSceneGO);
        }
    }
}