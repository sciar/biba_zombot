using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class TestARMenuState : BibaMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.TestAR;
            }
        }
    }
}