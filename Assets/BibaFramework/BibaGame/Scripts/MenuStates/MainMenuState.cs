using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class MainMenuState : BibaMenuState 
    {
        public override GameScene GameScene {
            get {
                return GameScene.Main;
            }
        }
    }
}