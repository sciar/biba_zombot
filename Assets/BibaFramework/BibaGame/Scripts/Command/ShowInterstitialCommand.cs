using strange.extensions.command.impl;
using ChartboostSDK;
using BibaFramework.BibaAnalytic;
using BibaFramework.Utility;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class ShowInterstitialCommand : Command
    {
        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        public override void Execute ()
        {
            if (BibaUtility.CheckForInternetConnection())
            {
                Chartboost.setShouldPauseClickForConfirmation(true);
                Chartboost.showInterstitial(CBLocation.Default);
            }
            else
            {
                SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Next);
            }
        }
    }
}