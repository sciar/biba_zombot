using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.Animations;

[CustomEditor(typeof(BibaGameView))]
public class DebugBibaGameView : Editor {
	
	public bool enableAutoBibaCanvasGroupActivation {
		set {
			_enableAutoBibaCanvasGroupActivation = value;
			if (!value)
				DeactivateLastGroup ();
		}
	}

	[HideInInspector] public static bool _enableAutoBibaCanvasGroupActivation;

	private BibaCanvasGroup lastGroup;
	private bool hasInitializedToSelectiondelegate;

	void OnEnable() {
		if (!hasInitializedToSelectiondelegate) {
			Selection.selectionChanged += OnSelectionChanged;
			hasInitializedToSelectiondelegate = true;
		}
	}

	public override void OnInspectorGUI () {
		enableAutoBibaCanvasGroupActivation = EditorGUILayout.Toggle ("Enable Auto Group Activation", _enableAutoBibaCanvasGroupActivation);
	}

	void OnSelectionChanged() {
		if (Application.isPlaying)
			return;
		if (!_enableAutoBibaCanvasGroupActivation)
			return;
		if (Selection.activeGameObject) {
			BibaCanvasGroup selected = lookForBibaCanvasGroup(Selection.activeGameObject);
			if (selected) {
				DeactivateLastGroup ();
				selected.canvasGroup.alpha = 1f;
				selected.canvasGroup.interactable = true;
				lastGroup = selected;
			} else {
				DeactivateLastGroup ();
			}
		}
		TryToGetCanvasGroupFromAnimatorState ();
	}

	void TryToGetCanvasGroupFromAnimatorState() {
		try {
			AnimatorState state = (AnimatorState)Selection.activeObject;
			if (!state) return;
			foreach(StateMachineBehaviour behaviour in state.behaviours) {
				BibaGameState bibaState = behaviour as BibaGameState;
				if (bibaState) {
					BibaCanvasGroup[] bibaCanvasGroups = ((BibaGameView)target).gameObject.GetComponentsInChildren<BibaCanvasGroup>(true);
					foreach(BibaCanvasGroup canvasGroup in bibaCanvasGroups) {
						if (canvasGroup.currentState == bibaState.currentState) {
							DeactivateLastGroup();
							canvasGroup.canvasGroup.alpha = 1f;
							canvasGroup.canvasGroup.interactable = true;
							lastGroup = canvasGroup;
							return;
						}
					}
					break;
				}
			}
		} catch {
		}
	}

	void DeactivateLastGroup() {
		if (lastGroup) {
			lastGroup.canvasGroup.alpha = 0f;
			lastGroup.canvasGroup.interactable = false;
		}
	}

	/// <summary>
	/// Looks for Biba Canvas Group.
	/// </summary>
	/// <returns>The first Biba Canvas Group found. NULL if parent tree contains no Biba Canvas Groups. </returns>
	/// <param name="obj">Object which or which any parent may contain a Biba Canvas Group.</param>

	BibaCanvasGroup lookForBibaCanvasGroup(GameObject obj) {
		if (!obj) {
			return null;
		}
		BibaCanvasGroup onObject = obj.GetComponent<BibaCanvasGroup> ();
		if (onObject) {
			return onObject;
		} else {
			if (obj.transform.parent) {
				return lookForBibaCanvasGroup (obj.transform.parent.gameObject);
			} else {
				return null;
			}
		}
	}
}
