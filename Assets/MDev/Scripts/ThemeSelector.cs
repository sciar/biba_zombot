using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThemeSelector : MonoBehaviour {

    public Sprite[] zombieThemes;
    public Sprite[] survivorThemes;
    private float imageSwapTimer;
    private float imageSwapTimerMax; // This will slowly grow to slow down the flipping
    private int rotationTotal = 0; // How many times we want to flip before swapping screens
    private int rotationMax = 20;
    private bool triggerOnce;

    private int maxThemeCount;
    private int themeCounter;

    public Animator anim;

    public BibaCanvasGroup bibaCanvasGroup;

    void OnEnable()
    {
        if (GameManager.Instance.victor == "Zombies")
            maxThemeCount = zombieThemes.Length;
        else
            maxThemeCount = survivorThemes.Length;

        themeCounter = 0; // Reset theme counter so we always start at 0
        rotationTotal = 0; // Reset every time we start back up
        triggerOnce = false; // Reset this so we only send one flag to the animator
        imageSwapTimerMax = 0.1f;
    }
    void OnDisable()
    {

    }
        
	
	void Update () {
        imageSwapTimerMax += 0.002f; // Slowly increment this to slow down the image swaps

        if (imageSwapTimer > 0)
            imageSwapTimer-= Time.deltaTime; // Every second we go down 1
        else
        {
            if (GameManager.Instance.victor == "robbers")
                GetComponent<Image>().sprite = zombieThemes[themeCounter];
            else
                GetComponent<Image>().sprite = survivorThemes[themeCounter];
            imageSwapTimer = imageSwapTimerMax;
            rotationTotal++;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.rouletteTick);

            // If theme counter is at the max theme count increment else reset
            if (themeCounter < maxThemeCount-1) 
                themeCounter++;
            else
                themeCounter = 0;
        }

        if (rotationTotal >= rotationMax && triggerOnce == false)
        {
            triggerOnce = true;
            anim.SetTrigger("Next");
        }

	}
}
