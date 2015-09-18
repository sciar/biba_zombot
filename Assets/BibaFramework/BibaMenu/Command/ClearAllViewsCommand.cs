using UnityEngine;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class ClearAllViewsCommand : Command 
    {
        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
            RemoveAllGameViews();
        }

        void RemoveAllGameViews()
        {
            foreach (var menuState in BibaSceneStack)
            {
                DestroyGameView(menuState);
            }

            BibaSceneStack.Clear();
        }
       
        void DestroyGameView(BibaMenuState menuState)
        {
            var gameSceneGO = GameObject.Find(menuState.GameScene.ToString());
            GameObject.DestroyImmediate(gameSceneGO);
        }
    }
}