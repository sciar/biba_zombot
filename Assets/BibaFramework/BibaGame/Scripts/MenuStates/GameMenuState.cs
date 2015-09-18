using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class GameMenuState : BibaMenuState 
    {
        public override GameScene GameScene {
            get {
                return GameScene.Game;
            }
        }
    }
}