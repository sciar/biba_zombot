using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class InactiveMenuState : SceneMenuState 
    {
        public override string SceneName {
            get {
                return BibaScene.Inactive;
            }
        }
    }
}