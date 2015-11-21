using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class PlayBGMCommand : Command
    {
        [Inject]
        public BibaBGM BibaBGM { get; set; }

        [Inject]
        public AudioServices AudioServices { get; set; }

        public override void Execute ()
        {
            if (BibaBGM == BibaBGM.none)
            {
                return;
            }

            AudioServices.PlayBGM(BibaBGM.ToString());
        }
    }
}