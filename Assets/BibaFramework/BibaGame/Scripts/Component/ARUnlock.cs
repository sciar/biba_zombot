using UnityEngine;
using System.Collections;
using BibaFramework.BibaGame;

[RequireComponent(typeof(SpriteRenderer))]
public class ARUnlock : MonoBehaviour 
{
	private SpriteRenderer _renderer;
	public SpriteRenderer SpriteRenderer
	{
		get {
			if (_renderer == null) {
				_renderer = GetComponent<SpriteRenderer> ();
			}
			return _renderer;
		}
	}
	public Transform targetCameraTransform;

	private Transform originalParent;
	private bool chasingTarget = false;

	void Start() {
		originalParent = transform.parent;
	}

	void Update () {
		if (chasingTarget) {
			transform.position = Vector3.Lerp (transform.position, targetCameraTransform.position, BibaGameConstants.TAG_FLOAT_TO_SCREEN_TIME);
			transform.rotation = Quaternion.Lerp (transform.rotation, targetCameraTransform.rotation, BibaGameConstants.TAG_FLOAT_TO_SCREEN_TIME);
			transform.localScale = Vector3.Lerp (transform.localScale, targetCameraTransform.localScale, BibaGameConstants.TAG_FLOAT_TO_SCREEN_TIME);
		}
	}

	public void ChaseTarget() {
		transform.SetParent (targetCameraTransform, true);
		chasingTarget = true;
	}

	public void Reset() {
		transform.SetParent (originalParent, true);
		chasingTarget = false;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
	}
}