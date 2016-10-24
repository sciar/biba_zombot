using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainLink : MonoBehaviour {

	public GameObject endChainTarget;
	public GameObject pivot;
	public GameObject trackGameObject;
	public List<Rigidbody2D> chainBodies;


	void OnEnable() {
		foreach (Transform t in transform) {
            t.position = pivot.transform.position;
		}
	}

	void Start() {
		chainBodies = new List<Rigidbody2D> ();
		foreach (Transform t in transform) {
			if (t.name == "Chain" || t.name == "Target") {
				chainBodies.Add(t.GetComponent<Rigidbody2D>());
			}
		}
	}

	void Update() {
        //Debug.LogError("Pivot "+pivot.transform.position);
        //Debug.LogError("Track Game Object " + trackGameObject.transform.position);
        endChainTarget.transform.position = trackGameObject.transform.position;
	}
}
