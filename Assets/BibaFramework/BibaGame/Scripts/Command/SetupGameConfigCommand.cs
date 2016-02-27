using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class SetupGameConfigCommand : Command
    {
        public override void Execute ()
        {
            SetFramerate();
        }

        void SetFramerate()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }
    }
}