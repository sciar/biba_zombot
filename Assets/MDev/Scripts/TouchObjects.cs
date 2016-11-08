using UnityEngine;
using System.Collections;

public class TouchObjects : MonoBehaviour {

    public GameObject shadingBox;
    private float growthRate = 0.1f;
    private bool grow = true;

	// Use this for initialization
	void OnEnable () {
        shadingBox.SetActive(false); // Make sure the shading box wont show up unless it gets activated
        transform.localScale = Vector3.zero;
	}
	
    public void turnOnShadingBox()
    {
        shadingBox.SetActive(true);
        grow = true;
    }

    public void turnOffShadingBox()
    {
        shadingBox.SetActive(false);
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (grow)
            transform.localScale += Vector3.one * growthRate;
        if (transform.localScale.x >= 1)
            grow = false;

    }
}
