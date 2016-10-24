using UnityEngine;
using System.Collections;

public class PhotoTakenText : MonoBehaviour {

    private float growthRate = 0.075f;
    private bool grow = true;

    void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        grow = true;
    }

    // Update is called once per frame
    void Update () {
        if (transform.localScale.x > 1)
            grow = false;
        else if (transform.localScale.x < 0)
            this.gameObject.SetActive(false);

        if (grow)
            transform.localScale += Vector3.one * growthRate;
        else
            transform.localScale -= Vector3.one * growthRate;

    }
}
