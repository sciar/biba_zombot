using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class GameMenuState : SceneMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.Game;
            }
        }
    }
}