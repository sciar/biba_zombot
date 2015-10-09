using strange.extensions.command.impl;
using BibaFramework.BibaMenu;
using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    public class SetupGameStateMachineCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject(BibaMenuConstants.BIBA_ROOT_CONTEXT_VIEW)]
        public GameObject RootContextView { get; set; }
        
        public override void Execute ()
        {
            var stateMachine = RootContextView.GetComponentInChildren<Animator>();
            injectionBinder.Bind<Animator>().To(stateMachine).ToName(BibaMenuConstants.BIBA_STATE_MACHINE).ToSingleton().CrossContext();
        }
    }
}

