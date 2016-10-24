using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class RemoveVuforiaCommand : Command
    {
        private const string VUFORIA_CAMERA_OBJECT = "TextureBufferCamera";

        public override void Execute ()
        {
            var go = GameObject.Find(VUFORIA_CAMERA_OBJECT);
            if (go != null)
            {
                GameObject.Destroy(go);
            }
        }
    }
}