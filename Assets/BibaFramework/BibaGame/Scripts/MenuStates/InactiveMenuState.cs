using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class InactiveMenuState : SceneMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.Inactive;
            }
        }
    }
}