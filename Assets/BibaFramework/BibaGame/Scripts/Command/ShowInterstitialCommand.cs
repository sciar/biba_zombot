using strange.extensions.command.impl;
using ChartboostSDK;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
    public class ShowInterstitialCommand : Command
    {
        public override void Execute ()
        {
            Chartboost.setShouldPauseClickForConfirmation(true);
            Chartboost.showInterstitial(CBLocation.Default);
        }
    }
}