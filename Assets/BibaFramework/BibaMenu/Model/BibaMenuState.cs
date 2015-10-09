using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
	public abstract class BibaMenuState : StateMachineBehaviour 
	{
        public abstract BibaScene GameScene { get; }
        public bool Popup;
        public bool FullScreen { get { return !Popup; } }

        public bool Replace;

    	// OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
        {
            animator.GetComponent<BibaMenuStateMachineView>().EnteredMenuState(this);
    	}

        public override string ToString()
        {
            return string.Format("[BibaMenuState: GameScene={0}, Popup={1}]", GameScene, Popup);
        }   
	}
}