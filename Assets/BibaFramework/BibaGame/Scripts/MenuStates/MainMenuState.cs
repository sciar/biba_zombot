using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class MainMenuState : BibaMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.Main;
            }
        }
    }
}