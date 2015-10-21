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
                var go = BibaSceneStack.GetTopGameObjectForTopMenuState();
                go.GetComponentInChildren<GraphicRaycaster>().enabled = true;
            }
        }
    }
}