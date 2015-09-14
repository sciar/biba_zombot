using UnityEngine;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class DestroyLastViewCommand : Command 
    {
        [Inject]
        public BibaSceneModel BibaSceneModel { get; set; }
        
        private BibaMenuState menuState { get { return BibaSceneModel.LastMenuState; } }

        public override void Execute ()
        {
            if (menuState != null)
            {
                DestroyGameView();
            }
        }

        void DestroyGameView()
        {
            var gameSceneGO = GameObject.Find(menuState.GameScene.ToString());
            GameObject.DestroyImmediate(gameSceneGO);
        }
    }
}