using BibaFramework.BibaAnalytic;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using ChartboostSDK;
using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.Experimental.Director;

namespace BibaFramework.BibaGame
{
    public class SetServicesCommand : Command
    {
        [Inject(BibaMenuConstants.BIBA_ROOT_CONTEXT_VIEW)]
        public GameObject RootContextView { get; set; }
        
		[Inject]
		public IAnalyticService AnalyticService { get; set; }

        public override void Execute ()
        {
            SetupStateMachine();
            SetupAudioService();
			SetupAnalyticService();
        }

        void SetupStateMachine()
        {
            var stateMachine = RootContextView.GetComponentInChildren<Animator>();
			injectionBinder.Bind<IAnimatorControllerPlayable>().To(stateMachine).ToName(BibaMenuConstants.BIBA_STATE_MACHINE).ToSingleton().CrossContext();
        }

        void SetupAudioService()
        {
            var audioService = RootContextView.GetComponentInChildren<AudioServices>();
            injectionBinder.Bind<AudioServices>().To(audioService).ToSingleton().CrossContext();
        }

		void SetupAnalyticService()
		{
			var flurryConfig = RootContextView.GetComponent<FlurryConfigs>();
			AnalyticService.SetupTracking(flurryConfig.FlurryIosKey, flurryConfig.FlurryAndroidKey);
		}
    }
}

