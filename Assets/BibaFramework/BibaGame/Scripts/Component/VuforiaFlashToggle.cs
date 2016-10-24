using UnityEngine;
using System.Collections;
using Vuforia;

namespace BibaFramework.BibaGame
{
	public class VuforiaFlashToggle : MonoBehaviour 
	{
		public void ToggleFlash(bool status)
		{
			CameraDevice.Instance.SetFlashTorchMode (status);	
		}
	}
}