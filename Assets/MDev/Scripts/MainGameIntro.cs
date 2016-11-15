using UnityEngine;
using System.Collections;

public class MainGameIntro : MonoBehaviour {

    public BibaCanvasGroup bibaCanvasGroup;
    public Animator gameAnim;

    // Use this for initialization
    void OnEnable () {
        bibaCanvasGroup.onCanvasGroupEnterSuscribers += playCreak;
    }
    void OnDisable()
    {
        bibaCanvasGroup.onCanvasGroupEnterSuscribers -= playCreak;
    }

    void playCreak(){
        AudioManager.Instance.PlaySingle(AudioManager.Instance.doorCreak);
    }
	
    public void setNextAnimator()
    {
        gameAnim.SetTrigger("Next");
    }
}
