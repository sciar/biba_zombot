using UnityEngine;
using System.Collections;

public class AnimationTriggerAudio : MonoBehaviour {

    public AudioClip clippie;
    public AudioClip clippie2;

    public void DoTheAudio()
    {
        AudioManager.Instance.PlaySFX(clippie);
    }
       
    public void DoTheAudio2()
    {
        AudioManager.Instance.PlaySFX(clippie2);
    }
}
