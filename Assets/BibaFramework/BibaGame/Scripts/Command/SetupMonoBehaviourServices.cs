using UnityEngine;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using ChartboostSDK;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
    public class SetupMonoBehaviourServices : Command
    {
        [Inject(BibaMenuConstants.BIBA_ROOT_CONTEXT_VIEW)]
        public GameObject RootContextView { get; set; }
        
        public override void Execute ()
        {
            SetupStateMachine();
            SetupAudioService();
            SetupChartBoostService();
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
    }
}

