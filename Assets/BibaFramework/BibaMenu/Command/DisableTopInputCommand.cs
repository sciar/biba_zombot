using UnityEngine;
using UnityEngine.UI;
using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class DisableTopInputCommand : Command 
    {
        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
            if (BibaSceneStack.Count > 0)
            {
                var gameSceneGO = GameObject.Find(BibaSceneStack.Peek().SceneName);
                gameSceneGO.GetComponentInChildren<GraphicRaycaster>().enabled = false;
            }
        }
    }
}