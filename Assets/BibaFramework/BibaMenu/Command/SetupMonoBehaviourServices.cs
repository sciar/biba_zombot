using UnityEngine;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using ChartboostSDK;

namespace BibaFramework.BibaMenu
{
    public class SetupMonoBehaviourServices : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject(BibaMenuConstants.BIBA_ROOT_CONTEXT_VIEW)]
        public GameObject RootContextView { get; set; }
        
        public override void Execute ()
        {
            SetupStateMachine();
            SetupAudioService();
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
            var chartBoostService = RootContextView.GetComponentInChildren<Chartboost>();
            injectionBinder.Bind<Chartboost>().To(chartBoostService).ToSingleton().CrossContext();

        }
    }
}

