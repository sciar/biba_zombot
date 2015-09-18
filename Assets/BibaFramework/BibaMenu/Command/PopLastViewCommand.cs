using UnityEngine;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class PopLastViewCommand : Command 
    {
        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
            if (BibaSceneStack.Count > 0)
            {
                RemoveLastGameView();
            }
        }

        void RemoveLastGameView()
        {
            DestroyGameView(BibaSceneStack.Pop());
        }

        void DestroyGameView(BibaMenuState menuState)
        {
            var gameSceneGO = GameObject.Find(menuState.GameScene.ToString());
            GameObject.DestroyImmediate(gameSceneGO);
        }
    }
}