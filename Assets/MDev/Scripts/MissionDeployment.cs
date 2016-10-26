using UnityEngine;
using System.Collections;

public class MissionDeployment : MonoBehaviour {

    private float growthRate = 0.1f;
    private bool grow = true;

	void Start () {
        transform.localScale = new Vector3(0, 0, 0);
	}

    void OnDisable()
    {
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x >= 1)
            grow = false;
        else if (transform.localScale.x < 0)
            Destroy(this.gameObject);
        
        if (grow)
            transform.localScale += Vector3.one * growthRate;

    }
}
