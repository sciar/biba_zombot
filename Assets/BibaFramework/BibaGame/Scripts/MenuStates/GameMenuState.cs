using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class GameMenuState : BibaMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.Game;
            }
        }
    }
}