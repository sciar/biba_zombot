using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
	public abstract class BaseMenuState : StateMachineBehaviour 
	{
		public abstract string SceneName { get; }
        public abstract bool FullScreen { get; } 

        [HideInInspector]
        public string EnterBGM = BibaBGM.None;

        [HideInInspector]
        public string ExitBGM = BibaBGM.None;

        [HideInInspector]
        public string EnterSFX = BibaSFX.None;

        [HideInInspector]
        public string ExitSFX = BibaSFX.None;

    	// OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
        {
            animator.GetComponent<MenuStateMachineView>().EnteredMenuState(this);
    	}
	}
}