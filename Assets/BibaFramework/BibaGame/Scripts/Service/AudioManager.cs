using UnityEngine;
using System.Collections;
using BibaFramework.BibaGame;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance = null; // This + Awake sets us up so we can use singletons

    public AudioSource sfxSource;
    public AudioSource bgMusic;

    public UnityEngine.AudioClip goSound;
    public UnityEngine.AudioClip buttonSFX;
    public UnityEngine.AudioClip poofSFX;
    public UnityEngine.AudioClip winJingle;
    public UnityEngine.AudioClip mainBGMusic;
    public UnityEngine.AudioClip cameraShutter;
    public UnityEngine.AudioClip rouletteTick;
    public UnityEngine.AudioClip radioCall;
    public UnityEngine.AudioClip missionPopup;
    public UnityEngine.AudioClip doorCreak;
    public UnityEngine.AudioClip zombieMoan;

    private GameObject introMusic; // This is how we're going to stop the intro music

    // Use this for initialization
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        AudioServices audiothing = GameObject.Find("AudioService").GetComponent<AudioServices>();
        audiothing.StopBGM();

        introMusic = GameObject.Find("SFXPrefab(Clone)");
        Destroy(introMusic);
    }

    public void PlaySingle(UnityEngine.AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void PlaySFX(params UnityEngine.AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);

        sfxSource.clip = clips[randomIndex];
        sfxSource.Play();

    }

    public void StartBGMusic()
    {
        bgMusic.clip = mainBGMusic;
        bgMusic.Play();
    }
    public void StopBGMusic()
    {
        bgMusic.Stop();
    }

}