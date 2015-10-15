using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class InactiveMenuState : SceneMenuState 
    {
        public override BibaScene BibaScene {
            get {
                return BibaScene.Inactive;
            }
        }
    }
}