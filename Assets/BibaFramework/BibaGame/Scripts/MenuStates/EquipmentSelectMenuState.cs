using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class EquipmentSelectMenuState : SceneMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.EquipmentSelect;
            }
        }
    }
}