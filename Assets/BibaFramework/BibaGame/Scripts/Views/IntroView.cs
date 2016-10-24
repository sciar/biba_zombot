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

            if (GameObject.Find("SFXPrefab(Clone)"))
            {
                // If we have any SFx playing do not run the music
            }
            else
            {// Music ahoy
                AudioServices.PlaySFX(sweetIntroMusic);
            }
		}
    }
}