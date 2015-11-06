using UnityEngine;
using System.Collections;

public enum BibaGameStatesEnum {
	Introduction,
	Instructions,
	GetReady,
	DropBiblet,
	DropAnimation,
	Events,
	SoecialBiblet,
	Scanner,
	ScanFail,
	ScanOk,
	CollectBiblet,
	MissionComplete,
	BibletsScore,
	AndSpecial,
	Milestones,
	GameEnd,
	//Events
	Tickle,
	Shower,
	Coloring,
	Feed,
	Wiggle,
	EventComplete,
	Reset,
	//Add new here
	NULL
}

public class BibaGameState : StateMachineBehaviour {
	
	public BibaGameStatesEnum currentState;

	private BibaCanvasGroup currentCanvasGroup;
	private BibaCanvasGroup previousCanvasGroup;
	private BibaGameView logic;

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		// Get reference to RelayGameLogic only if needed
		if (logic == null) {
			logic = animator.GetComponent<BibaGameView> ();
		}

		logic.currentGameState = currentState;

		// Get the Canvas Group bound to the current RelayGameState, else throw a warning.
		if (logic.statesToBibaCanvasGroups.ContainsKey (currentState)) {
			currentCanvasGroup = logic.statesToBibaCanvasGroups [currentState];
		} else {
			Debug.LogError("Key not found in GameLogic Dictionary.");
		}

		// Add state dependent functionality here:
		switch (currentState) {
		default:
			break;
		}

		if (currentCanvasGroup != null) {
			currentCanvasGroup.gameObject.SetActive (true);
			currentCanvasGroup.canvasGroup.FadeAlphaTo (1f, currentCanvasGroup.entryFadeDuration, currentCanvasGroup.entryDelay);
			currentCanvasGroup.canvasGroup.EnableInteraction (true);
			logic.statesToBibaCanvasGroups [currentState].CanvasGroupIsActive();
		} else {
			Debug.LogError("Canvas Group is null");
		}
	}

	//OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		// Add state dependent functionality here:
		switch (currentState) {
		default:
			break;
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		// Add state dependent functionality here:
		switch (currentState) {
		default:
			break;
		}

		if (currentCanvasGroup != null) {
			previousCanvasGroup = currentCanvasGroup;
			previousCanvasGroup.canvasGroup.FadeAlphaTo(0f, currentCanvasGroup.exitFadeDuration, currentCanvasGroup.exitDelay, DisablePreviousCanvasGroup);
			previousCanvasGroup.canvasGroup.EnableInteraction(false);
		}
	}

	void DisablePreviousCanvasGroup() {
		previousCanvasGroup.gameObject.SetActive (false);
	}
}
