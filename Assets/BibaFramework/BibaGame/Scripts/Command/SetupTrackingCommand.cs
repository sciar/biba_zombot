using UnityEngine;
using BibaFramework.BibaAnalytic;
using BibaFramework.BibaMenu;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
    public class SetupTrackingCommand : Command
    {
        [Inject(BibaMenuConstants.BIBA_ROOT_CONTEXT_VIEW)]
        public GameObject RootContextView { get; set; }

        [Inject]
        public IBibaAnalyticService AnalyticService { get; set; }

        public override void Execute ()
        {
            SetupTrackingService();
        }

        void SetupTrackingService()
        {
            var flurryConfig = RootContextView.GetComponent<FlurryConfigs>();
            AnalyticService.StartSession(flurryConfig.FlurryIosKey, flurryConfig.FlurryAndroidKey);
        }
    }
}