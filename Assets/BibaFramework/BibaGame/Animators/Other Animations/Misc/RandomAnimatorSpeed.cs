using UnityEngine;
using System.Collections;

public class RandomAnimatorSpeed : MonoBehaviour {

	public float minValue = 0.5f;
	public float maxValue = 1f;

	void Start() {
		GetComponent<Animator> ().speed = Random.Range (minValue, maxValue);
	}
}
