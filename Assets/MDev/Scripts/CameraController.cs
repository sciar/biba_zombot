using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject ARCamera;

    public WebCamTexture mCamera;
    public GameObject plane;

    public GameObject BG;

    void OnEnable(){ // Enable the camera and disable the background
        Debug.LogError("We turn on the camera");
        ARCamera.SetActive(true);
        BG.SetActive(false);
    }
        
    void Start ()
    {
        mCamera = new WebCamTexture ();
        plane.GetComponent<Renderer>().material.mainTexture = mCamera;
        mCamera.Play ();
    }

    void OnDisable(){ // Revert the changes
        ARCamera.SetActive(false);
        BG.SetActive(true);
    }
       
}