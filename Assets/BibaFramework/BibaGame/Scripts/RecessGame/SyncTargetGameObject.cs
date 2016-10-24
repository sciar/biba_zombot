using UnityEngine;
using System.Collections;

public class SyncTargetGameObject : MonoBehaviour {

	public GameObject targetGameObject;

	void OnEnable() {
		targetGameObject.SetActive (true);
	}

	void OnDisable() {
		targetGameObject.SetActive (false);
	}
}