using strange.extensions.command.impl;
using BibaFramework.BibaNetwork;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class SetupGameConfigCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public ICDNService CDNService { get; set; }

        public override void Execute ()
        {
            CDNService.UpdateFromCDN();
            SetFramerate();
        }

        void SetFramerate()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }
    }
}