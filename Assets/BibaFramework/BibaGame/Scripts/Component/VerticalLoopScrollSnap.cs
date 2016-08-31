using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VerticalLoopScrollSnap : MonoBehaviour {

	public float snapStrength = 5f;
	public RectTransform content;

	private RectTransform rectTransform;
	private RectTransform[] childTransforms;
	private LayoutElement[] childLayouts;
	private int numberOfChildren;
	private float childHeight;
	private float childWidth;
	private int closestElement;

	void Start() {
		rectTransform = GetComponent<RectTransform> ();
		childWidth = rectTransform.rect.width;
		childHeight = rectTransform.rect.height;
		childTransforms = content.GetComponentsInChildren<RectTransform> ();
		childLayouts = content.GetComponentsInChildren<LayoutElement> ();
		numberOfChildren = childLayouts.Length;
		FitLayoutsToChildHeight ();
		FitContentHeightToNumberOfChildren ();
	}

	void Update() {
		closestElement = GetClosestElement();
		if (!Input.GetMouseButton (0)) {
			SnapToClosestElement ();
		}

	}

	void FitLayoutsToChildHeight() {
		foreach (LayoutElement element in childLayouts) {
			element.minHeight = childHeight;
		}
	}

	void FitContentHeightToNumberOfChildren() {
		rectTransform.sizeDelta = new Vector2 (childWidth, childHeight);
		content.sizeDelta = new Vector2 (childWidth, (childHeight * numberOfChildren));
		content.anchoredPosition = new Vector2 (0f, (numberOfChildren / 2) * childHeight);
	}

	void SnapToClosestElement() {
		content.anchoredPosition = Vector2.Lerp (content.anchoredPosition, new Vector2 (0f, closestElement * childHeight), snapStrength*Time.deltaTime);
	}

	int GetClosestElement() {
		if (content.anchoredPosition.y > 0) {
			return ((int)content.anchoredPosition.y + ((int)childHeight / 2)) / (int)childHeight;
		} else {
			return ((int)content.anchoredPosition.y - ((int)childHeight / 2)) / (int)childHeight;
		}
	}
}
