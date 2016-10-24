using BibaFramework.BibaMenu;
using UnityEngine.UI;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class PrivacyStatementView : SceneMenuStateView
    {
		void Start() {
			Application.targetFrameRate = 60;
			QualitySettings.vSyncCount = 0;
		}
    }
}
