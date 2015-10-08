using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class InactiveMenuState : BibaMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.Inactive;
            }
        }
    }
}