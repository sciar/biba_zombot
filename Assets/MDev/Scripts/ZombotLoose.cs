using UnityEngine;
using System.Collections;

public class ZombotLoose : MonoBehaviour {

    public AudioClip zombieMoan;

	// Use this for initialization
	void OnEnable () {
        AudioManager.Instance.PlaySFX(zombieMoan);
	}
	
}
