using UnityEngine;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using ChartboostSDK;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
    public class SetupServicesCommand : Command
    {
        [Inject(BibaMenuConstants.BIBA_ROOT_CONTEXT_VIEW)]
        public GameObject RootContextView { get; set; }
        
		[Inject]
		public IAnalyticService AnalyticService { get; set; }

        public override void Execute ()
        {
            SetupStateMachine();
            SetupAudioService();
            SetupChartBoostService();
			SetupAnalyticService();
        }

        void SetupStateMachine()
        {
            var stateMachine = RootContextView.GetComponentInChildren<Animator>();
            injectionBinder.Bind<Animator>().To(stateMachine).ToName(BibaMenuConstants.BIBA_STATE_MACHINE).ToSingleton().CrossContext();
        }

        void SetupAudioService()
        {
            var audioService = RootContextView.GetComponentInChildren<AudioServices>();
            injectionBinder.Bind<AudioServices>().To(audioService).ToSingleton().CrossContext();
        }

        void SetupChartBoostService()
        {
            var chartBoostService = RootContextView.GetComponentInChildren<ChartBoostService>();
            injectionBinder.Bind<ChartBoostService>().To(chartBoostService).ToSingleton().CrossContext();
        }

		void SetupAnalyticService()
		{
			var flurryConfig = RootContextView.GetComponent<FlurryConfigs>();
			AnalyticService.StartTracking(flurryConfig.FlurryIosKey, flurryConfig.FlurryAndroidKey);
		}
    }
}

