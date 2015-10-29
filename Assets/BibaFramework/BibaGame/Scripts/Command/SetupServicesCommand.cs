using UnityEngine;
using BibaFramework.BibaAnalytic;
using BibaFramework.BibaMenu;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
    public class SetupServicesCommand : Command
    {
        [Inject(BibaMenuConstants.BIBA_ROOT_CONTEXT_VIEW)]
        public GameObject RootContextView { get; set; }
        
        public override void Execute ()
        {
            SetupTrackingService();
        }

        void SetupTrackingService()
        {
            var flurryConfig = RootContextView.GetComponent<FlurryConfigs>();
            
            var flurryAnalytics = new FlurryAnalyticService(flurryConfig.FlurryIosKey, flurryConfig.FlurryAndroidKey);
            injectionBinder.Bind<IBibaAnalyticService>().To(flurryAnalytics).ToSingleton().CrossContext();
        }
    }
}

