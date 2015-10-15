using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class MainMenuState : SceneMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.Main;
            }
        }
    }
}