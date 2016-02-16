using UnityEngine;
using System.Collections;

public class ChemaDebugObject : MonoBehaviour {

	public float fastTimeScaleSpeed = 10f;
	public float slowTimeScaleSpeed = 0.1f;
	
	void Update () {
#if UNITY_EDITOR
		if (Input.GetKey (KeyCode.Space)) {
			Time.timeScale = fastTimeScaleSpeed;
		} else if (Input.GetKey(KeyCode.Period)) {
			Time.timeScale = slowTimeScaleSpeed;
		} else {
			Time.timeScale = 1f;
		}
#endif
	}
}
