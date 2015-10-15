using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class IntroMenuState : SceneMenuState 
    {
        public override BibaScene GameScene {
            get 
            {
                return BibaScene.Intro;
            }
        }
    }
}