using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HowToScrollSnap : MonoBehaviour {

	public RectTransform content;
	public float screenSize = 640f;
	public Image[] dots;
	public Sprite SelectedDot;
	public Sprite FreeDot;
	public Text skipButtonText;
	public Button prevButton;
	public Button nextButton;
	public BibaLocalizedServiceView loc;

	public int z = 0;
	private bool b_dragging = false;
	private bool b_moving = false;
	private int i_numberOfScreens = 3;
	private int currentScreen = -1;
	private Vector2 lastMousePos;
	private ScrollRect scrollRect;
	private bool commandGiven = false;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;
		scrollRect = GetComponent<ScrollRect> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 v2AnchoredPos = content.anchoredPosition;
		z = (int)Mathf.Abs((v2AnchoredPos.x-(screenSize/2f)) / screenSize);
		if (!b_dragging && !b_moving) {
			v2AnchoredPos.x = z * -screenSize;
			content.anchoredPosition = Vector2.Lerp (content.anchoredPosition,
			                                         v2AnchoredPos,
			                                         4f * Time.deltaTime);
		}
		if (currentScreen != z) {
			currentScreen = z;
			foreach (Image dot in dots) {
				dot.sprite = FreeDot;
			}
			dots[currentScreen].sprite = SelectedDot;
			if (currentScreen == i_numberOfScreens) {
				skipButtonText.text = loc.GetLocalizedText("common_ok").ToUpper();
			} else {
				skipButtonText.text = loc.GetLocalizedText("common_skip").ToUpper();
			}
		}
		Vector2 currentMousePos = Input.mousePosition;
		if (Input.GetMouseButtonUp (0)) {
			commandGiven = false;
			if (!b_moving) {
				scrollRect.enabled = true;
			}
		}
		if (b_dragging) {
			if (!b_moving && !commandGiven) {
				Vector2 dragSpeed = currentMousePos-lastMousePos;
				if (dragSpeed.x < -10f && z < i_numberOfScreens) {
					StartCoroutine(Next());
					commandGiven = true;
				} else if (dragSpeed.x > 10f && z > 0) {
					StartCoroutine(Prev());
					commandGiven = true;
				}
			}
		}
		lastMousePos = Input.mousePosition;
	}

	public void DragEvent(bool state) {
		b_dragging = state;
	}

	public void NextScreen() {
		if (!b_moving && !b_dragging && z < i_numberOfScreens) {
			StartCoroutine (Next ());
		}
	}

	IEnumerator Next() {
		b_moving = true;
		nextButton.interactable = false;
		prevButton.interactable = false;
		scrollRect.enabled = false;
		int targetZ = z + 1;
		for (float f = 0f; f<0.4f; f+=Time.deltaTime) {
			content.anchoredPosition = Vector2.Lerp (content.anchoredPosition,
			                                         new Vector2(targetZ * -screenSize, content.anchoredPosition.y),
			                                         f);
			yield return null;
		}
		b_moving = false;
		nextButton.interactable = true;
		prevButton.interactable = true;
		if (!commandGiven)
			scrollRect.enabled = true;
	}

	public void PrevScreen() {
		if (!b_moving && !b_dragging && z > 0) {
			StartCoroutine (Prev ());
		}
	}
	
	IEnumerator Prev() {
		b_moving = true;
		nextButton.interactable = false;
		prevButton.interactable = false;
		scrollRect.enabled = false;
		int targetZ = z - 1;
		for (float f = 0f; f<0.4f; f+=Time.deltaTime) {
			content.anchoredPosition = Vector2.Lerp (content.anchoredPosition,
			                                         new Vector2(targetZ * -screenSize, content.anchoredPosition.y),
			                                         f);
			yield return null;
		}
		b_moving = false;
		nextButton.interactable = true;
		prevButton.interactable = true;
		if (!commandGiven)
			scrollRect.enabled = true;
	}
}
