using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class StartMenuState : SceneMenuState 
    {
        public override BibaScene BibaScene {
            get {
                return BibaScene.Start;
            }
        }
    }
}