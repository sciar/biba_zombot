using BibaFramework.BibaGame;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
	public abstract class SceneMenuState : BaseMenuState 
	{
        public bool Popup;
        public override bool FullScreen { get { return !Popup; } }

        public ScreenOrientation Orientation = ScreenOrientation.Portrait;
	}
}