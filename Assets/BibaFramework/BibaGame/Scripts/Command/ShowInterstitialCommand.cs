using strange.extensions.command.impl;
using ChartboostSDK;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
    public class ShowInterstitialCommand : Command
    {
        [Inject]
        public ChartBoostService ChartBoostService { get; set; }

        public override void Execute ()
        {
            ChartBoostService.showInterstitial(CBLocation.Default);
        }
    }
}