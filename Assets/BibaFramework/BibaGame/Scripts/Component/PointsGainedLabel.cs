using UnityEngine;
using strange.extensions.signal.impl;

public class PointsGainedLabel : MonoBehaviour 
{
	public Signal PointsGainedSignal = new Signal ();

	public void DispatchPointsGainedSignal()
	{
		PointsGainedSignal.Dispatch ();
		Destroy (this.gameObject);
	}
}
