using strange.extensions.command.impl;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public class SetupGameConfigCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService LoaderService { get; set; }

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