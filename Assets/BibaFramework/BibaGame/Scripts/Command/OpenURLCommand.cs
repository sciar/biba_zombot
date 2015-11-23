using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class OpenURLCommand : Command
    {
        [Inject]
        public string URL { get; set; }

        public override void Execute ()
        {
            Application.OpenURL(URL);
        }
    }
}