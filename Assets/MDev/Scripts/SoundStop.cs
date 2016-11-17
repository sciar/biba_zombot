using UnityEngine;
using System.Collections;
using BibaFramework.BibaGame;

public class SoundStop : MonoBehaviour {

    private GameObject introMusic;

    private void Awake()
    {
        AudioServices audiothing = GameObject.Find("AudioService").GetComponent<AudioServices>();
        audiothing.StopBGM();

        introMusic = GameObject.Find("SFXPrefab(Clone)");
        Destroy(introMusic);
    }
       
}
