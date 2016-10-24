using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class PlayBGMCommand : Command
    {
        [Inject]
        public string BGMName { get; set; }

        [Inject]
        public AudioServices AudioServices { get; set; }

        public override void Execute ()
        {
            if (BGMName == BibaBGM.None)
            {
                return;
            }

            AudioServices.PlayBGM(BGMName);
        }
    }
}