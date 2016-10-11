using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

namespace BibaFramework.BibaGame
{
	public class VuforiaFlashToggle : MonoBehaviour 
	{
		private Toggle _toggle;
		private Toggle Toggle {
			get {
				if(_toggle == null)
				{
					_toggle = GetComponent<Toggle> ();
				}
				return _toggle;
			}
		}

		void OnEnable()
		{
			Toggle.isOn = false;
		}
			
		void OnDisable()
		{
			Toggle.isOn = false;
		}

		public void ToggleFlash(bool status)
		{
			CameraDevice.Instance.SetFlashTorchMode (status);	
		}
	}
}