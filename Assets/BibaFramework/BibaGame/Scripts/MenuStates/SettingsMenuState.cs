using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class SettingsMenuState : SceneMenuState 
    {
        public override BibaScene BibaScene {
            get {
                return BibaScene.Settings;
            }
        }
    }
}