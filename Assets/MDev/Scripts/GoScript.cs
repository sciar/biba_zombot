using UnityEngine;
using System.Collections;

public class GoScript : MonoBehaviour {

    public Animator anim;


    public void GoSound()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.goSound);
    }
    public void GoToGame()
    {
        anim.SetTrigger("Next");
    }
}
