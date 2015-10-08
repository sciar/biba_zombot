using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class StartMenuState : BibaMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.Start;
            }
        }
    }
}