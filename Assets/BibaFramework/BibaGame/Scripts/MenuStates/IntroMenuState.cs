using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class IntroMenuState : BibaMenuState 
    {
        public override GameScene GameScene {
            get {
                return GameScene.Intro;
            }
        }
    }
}