using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
#endif

[ExecuteInEditMode]
public class DebugBibaGameView : MonoBehaviour {
	#if UNITY_EDITOR
	public bool enableAutoBibaCanvasGroupActivation {
		set {
			_enableAutoBibaCanvasGroupActivation = value;
			if (!value) {
				DeactivateLastGroup ();
			}
		}
		get {
			return _enableAutoBibaCanvasGroupActivation;
		}
	}

	[HideInInspector] public bool _enableAutoBibaCanvasGroupActivation;

	private BibaCanvasGroup lastGroup;

	void OnEnable() {
		Selection.selectionChanged += OnSelectionChanged;
	}

	void OnDisable() {
		Selection.selectionChanged -= OnSelectionChanged;
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
					BibaCanvasGroup[] bibaCanvasGroups = GetComponentsInChildren<BibaCanvasGroup>(true);
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
	#endif
}

#if UNITY_EDITOR
[CustomEditor (typeof(DebugBibaGameView))]
public class DebugBibaGameViewEditor : Editor {
	
	public override void OnInspectorGUI () {
		DebugBibaGameView t = (DebugBibaGameView)target;
		t.enableAutoBibaCanvasGroupActivation = EditorGUILayout.Toggle ("Enable Auto Group Activation", t.enableAutoBibaCanvasGroupActivation);
	}
}
#endif
