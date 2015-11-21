using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class PlaySFXCommand : Command
    {
        [Inject]
        public BibaSFX BibaSFX { get; set; }

        [Inject]
        public AudioServices AudioServices { get; set; }

        public override void Execute ()
        {
            if (BibaSFX == BibaSFX.none)
            {
                return;
            }

            AudioServices.PlaySFX(BibaSFX.ToString());
        }
    }
}