using UnityEngine;
using System.Collections;
using Vuforia;

public class TriggerFocus : MonoBehaviour {
	public void Trigger() {
		CameraDevice.Instance.SetFocusMode (CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
	}
}
