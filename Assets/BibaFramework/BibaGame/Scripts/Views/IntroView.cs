using BibaFramework.BibaMenu;
using UnityEngine.UI;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class IntroView : SceneMenuStateView
    {
		void Start() {
			Application.targetFrameRate = 60;
			QualitySettings.vSyncCount = 0;
			Screen.orientation = ScreenOrientation.Portrait;
			AudioServices.PlayBGM ("sickos_menubgm");
		}
    }
}