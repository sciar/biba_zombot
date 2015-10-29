using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class OpenAboutBibaURLCommand : Command
    {
        public override void Execute ()
        {
            Application.OpenURL(BibaGameConstants.BIBA_PRIVACY_POLICY_URL);
        }
    }
}