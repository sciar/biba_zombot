using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using BibaFramework.BibaMenu;

public class RecessHowTo : MonoBehaviour {
	public RectTransform horizontalLayout;
	public Transform dotsParent;
	public int numberOfElements = 9;
	public int canvasWidth = 2048;
	public float scrollSpeed = 6f;
	public Text nextButtonText;
	public Sprite dotOn;
	public Sprite dotOff;

	private int currentIndex = 0;
	private Animator menuStateAnimator;

	void Start() {
		menuStateAnimator = GameObject.FindObjectOfType<MenuStateMachineView> ().GetComponent<Animator> ();
	}

	void Update() {
		Vector2 targetPosition = new Vector2 (-currentIndex * canvasWidth, horizontalLayout.anchoredPosition.y);
		horizontalLayout.anchoredPosition = Vector2.Lerp (horizontalLayout.anchoredPosition, targetPosition, scrollSpeed * Time.deltaTime);
	}

	void UpdateDots() {
		foreach (Transform dot in dotsParent) {
			if (dot.GetSiblingIndex() == currentIndex) {
				dot.GetComponent<Image>().sprite = dotOn;
			} else {
				dot.GetComponent<Image>().sprite = dotOff;
			}
		}
	}

	public void NextScreen() {
		if (currentIndex >= numberOfElements-1) {
			menuStateAnimator.SetTrigger("Next");
			return;
		}
		currentIndex++;
		UpdateDots ();
		if (currentIndex == numberOfElements - 1) {
			nextButtonText.text = "PLAY!";
		}
	}

	public void PreviousScreen() {
		if (currentIndex <= 0) {
			menuStateAnimator.SetTrigger("Back");
			return;
		}
		if (currentIndex == numberOfElements - 1) {
			nextButtonText.text = "NEXT";
		}
		currentIndex--;
		UpdateDots ();
	}
}
