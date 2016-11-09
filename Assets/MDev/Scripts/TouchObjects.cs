using UnityEngine;
using System.Collections;

public class TouchObjects : MonoBehaviour {

    public GameObject shadingBox;
    public GameObject missionText;
    private float growthRate = 0.1f;


	// Use this for initialization
	void OnEnable () {
        shadingBox.SetActive(false); // Make sure the shading box wont show up unless it gets activated
        missionText.SetActive(false); // Makes sure the text is off at the start
        //transform.localScale = Vector3.zero;
	}
	
    public void turnOnShadingBox()
    {
        shadingBox.transform.localScale = Vector3.zero;
        shadingBox.SetActive(true);
        missionText.transform.localScale = Vector3.zero;
        missionText.SetActive(true);
    }

    public void turnOffShadingBox()
    {
        shadingBox.SetActive(false);
        missionText.SetActive(false);
        //transform.localScale = Vector3.zero;
    }
      
    void Update()
    {
        if (shadingBox.transform.localScale.x < 1)
        {
            shadingBox.transform.localScale += Vector3.one * growthRate;
        }
        if (missionText.transform.localScale.x < 1)
        {
            missionText.transform.localScale += Vector3.one * growthRate;
        }
    }
}
