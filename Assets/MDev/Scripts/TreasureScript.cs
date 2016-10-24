using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TreasureScript : MonoBehaviour {

    private float timer;
    private float timerTotal;
    private float speed;
    private float direction;
    private int spinChoice;
    private int rotateSpeed = 4;

    public Sprite[] potentialLooks;

	// Use this for initialization
	void Start () {
        timer = Random.Range(1f,2f);
        timerTotal = timer; // Stores the timer value so we can use percentages of it
        speed = 6.5f;
        direction = Random.Range(-0.8f, 0.8f);

        if (direction > 0)
            spinChoice = 1;
        else
            spinChoice = -1;
        
        this.GetComponent<Image>().sprite = potentialLooks[Random.Range(0, potentialLooks.Length)]; // Randomizes a sprite to use
	}
	
	// Update is called once per frame
	void Update () {
        var temporaryPosition = transform.position;
        if (timer > timerTotal * 0.9f)
            transform.position = temporaryPosition + new Vector3(direction,1,0) * speed * Time.deltaTime;
        else
            transform.position = temporaryPosition + new Vector3(direction,-1,0) * speed * Time.deltaTime;

        if (timer > 0) // If our timer is above zero lets reduce it
            timer -= Time.deltaTime;
        else // If it's not we destroy the object
        {
            GameManager.Instance.LootPileGrow();// Tells the game manager we've gained another Loot pile point
            Destroy(this.gameObject);
        }

        transform.Rotate (Vector3.forward * -90 * rotateSpeed * Time.deltaTime); // Rotates randomly right/left

        if (GameManager.Instance.gameTime < 0.1f) // If a new round starts destroy all the remaining treasure
            Destroy(this.gameObject); // Sloppy I know but no time to build a proper tracking list - Sorry future coder
	}
}
