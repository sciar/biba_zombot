using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class HelpBubblesGroupView : View {

	public Animator animator;
	[HideInInspector] public bool bubblesEnabled;
	public bool overrideBubble = false;
	public bool overrideValue = false;

	void OnEnable() {
		if (!overrideBubble) {
			SetAnimatorBoolean (bubblesEnabled);
		} else {
			SetAnimatorBoolean (overrideValue);
		}
	}

	public void SetAnimatorBoolean(bool value) {
		animator.SetBool ("Enabled", value);
	}

	public void SetAnimatorBooleanIfEnabled(bool value) {
		if (bubblesEnabled) {
			animator.SetBool ("Enabled", value);
		}
	}
}
