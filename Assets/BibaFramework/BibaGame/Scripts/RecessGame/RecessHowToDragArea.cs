using UnityEngine;
using System.Collections;

public class RecessHowToDragArea : MonoBehaviour {
	public RecessHowTo howToController;
	public float distanceTreshold = 20f;
	private Vector2 touchOrigin;
	private bool hasUpdated = true;
	public void BeginDrag() {
		touchOrigin = Input.mousePosition;
		hasUpdated = false;
	}
	public void UpdateDrag() {
		if (!hasUpdated) {
			Vector2 newTouchOrigin = Input.mousePosition;
			//Debug.LogError ("UPDATEDRAG "+Vector2.Distance(newTouchOrigin, newTouchOrigin).ToString());
			if (Vector2.Distance(newTouchOrigin, touchOrigin) >= distanceTreshold) {
				if (newTouchOrigin.x < touchOrigin.x) {
					howToController.NextScreen();
					hasUpdated = true;
				} else {
					howToController.PreviousScreen();
					hasUpdated = true;
				}
			}
		}
	}
}
