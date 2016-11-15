using BibaFramework.BibaMenu;
using UnityEngine.UI;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class IntroView : SceneMenuStateView
    {
        public AudioClip sweetIntroMusic;
        private AudioSource audio;

		void Start() {
			Application.targetFrameRate = 60;
			QualitySettings.vSyncCount = 0;
            audio = GetComponent<AudioSource>();

            if (!AudioServices.IsIntroPlaying())
            {
                AudioServices.PlayIntro(sweetIntroMusic);
            }

		}
    }
}