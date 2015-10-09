using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class IntroMenuState : BibaMenuState 
    {
        public override BibaScene GameScene {
            get 
            {
                return BibaScene.Intro;
            }
        }
    }
}