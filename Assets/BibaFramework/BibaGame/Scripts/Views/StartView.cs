using BibaFramework.BibaMenu;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class StartView : SceneMenuStateView
    {
		void Start() {
			if (SystemInfo.graphicsMemorySize < 1536) {
				QualitySettings.SetQualityLevel (0, true);
			}
			#if UNITY_IOS
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				if (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPodTouch5Gen ||
				    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone4 ||
				    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone4S) {
					QualitySettings.SetQualityLevel (0, true);
				}
			}
			#endif
		}
    }
}