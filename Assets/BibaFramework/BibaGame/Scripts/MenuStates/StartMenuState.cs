using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class StartMenuState : SceneMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.Start;
            }
        }
    }
}