using UnityEngine;
using System.Collections;

public class FreedomText : MonoBehaviour {

    private float growthRate = 0.035f;
    private bool grow = true;

	void Start () {
        transform.localScale = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x > 1)
            grow = false;
        else if (transform.localScale.x < 0)
            Destroy(this.gameObject);
        
        if (grow)
            transform.localScale += Vector3.one * growthRate;
        else
            transform.localScale -= Vector3.one * growthRate;

    }
}
