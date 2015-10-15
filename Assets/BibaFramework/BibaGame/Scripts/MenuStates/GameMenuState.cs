using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class GameMenuState : SceneMenuState 
    {
        public override BibaScene BibaScene {
            get {
                return BibaScene.Game;
            }
        }
    }
}