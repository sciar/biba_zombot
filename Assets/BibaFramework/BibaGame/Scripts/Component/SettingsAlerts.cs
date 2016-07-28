using UnityEngine;
using System.Collections;
using BibaFramework.BibaGame;

public class SettingsAlerts : MonoBehaviour {
	
	public SettingsView settingsView;
	public Animator alertAnimator;

	public void EraseModel() {
		settingsView.ResetDeviceSignal.Dispatch ();
	}
	
	public void OpenPrompt() {
		alertAnimator.SetBool ("Enabled", true);
	}
	
	public void ClosePrompt(bool value) {
		if (value) {
			EraseModel();
		}
		alertAnimator.SetBool ("Enabled", false);
	}
}
