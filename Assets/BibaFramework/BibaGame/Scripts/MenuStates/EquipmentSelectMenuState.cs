using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectMenuState : BibaMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.EquipmentSelect;
            }
        }
    }
}