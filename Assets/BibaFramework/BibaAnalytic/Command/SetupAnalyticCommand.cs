using strange.extensions.command.impl;
using BibaFramework.BibaMenu;
using UnityEngine;

namespace BibaFramework.BibaAnalytic
{
    public class SetupAnalyticCommand : Command
    {
        [Inject(BibaMenuConstants.BIBA_ROOT_CONTEXT_VIEW)]
        public GameObject RootContextView { get; set; }
        
        public override void Execute ()
        {
            var flurryConfig = RootContextView.GetComponent<FlurryConfigs>();
            
            var flurryAnalytics = new FlurryAnalyticService(flurryConfig.FlurryIosKey, flurryConfig.FlurryAndroidKey);
            injectionBinder.Bind<IBibaAnalyticService>().To(flurryAnalytics).ToSingleton().CrossContext();
        }
    }
}

