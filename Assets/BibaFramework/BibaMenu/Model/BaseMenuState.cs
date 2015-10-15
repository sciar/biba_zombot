using UnityEngine;

namespace BibaFramework.BibaMenu
{
	public abstract class BaseMenuState : StateMachineBehaviour 
	{
		public abstract string SceneName { get; }
        public abstract bool FullScreen { get; } 

        public bool Replace;
        
    	// OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
        {
            animator.GetComponent<MenuStateMachineView>().EnteredMenuState(this);
    	}
	}
}