using BibaFramework.BibaMenu;
using UnityEngine.UI;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class IntroView : SceneMenuStateView
    {
        public AudioClip sweetIntroMusic;

		void Start() {
			Application.targetFrameRate = 60;
			QualitySettings.vSyncCount = 0;
            AudioServices.PlaySFX(sweetIntroMusic);
		}
    }
}