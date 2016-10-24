using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
	public class SetTrackingServicesCommand : Command
	{
		[Inject(BibaMenuConstants.BIBA_ROOT_CONTEXT_VIEW)]
		public GameObject RootContextView { get; set; }

		[Inject]
		public IDeviceAnalyticService AnalyticService { get; set; }

		public override void Execute ()
		{
			SetupAnalyticService();
		}

		void SetupAnalyticService()
		{
			var flurryConfig = RootContextView.GetComponent<FlurryConfigs>();
			AnalyticService.SetupTracking(flurryConfig.FlurryIosKey, flurryConfig.FlurryAndroidKey);
		}
	}
}