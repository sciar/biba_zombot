using UnityEngine;
using UnityEngine.UI;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class EnableTopInputCommand : Command 
    {
        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
            if (BibaSceneStack.Count > 0)
            {
                var gameSceneGO = GameObject.Find(BibaSceneStack.Peek().GameScene.ToString());
                gameSceneGO.GetComponentInChildren<GraphicRaycaster>().enabled = true;
            }
        }
    }
}