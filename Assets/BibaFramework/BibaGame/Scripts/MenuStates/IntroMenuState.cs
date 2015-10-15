using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class IntroMenuState : SceneMenuState 
    {
        public override BibaScene BibaScene {
            get 
            {
                return BibaScene.Intro;
            }
        }
    }
}