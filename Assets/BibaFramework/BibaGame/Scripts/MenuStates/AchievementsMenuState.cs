using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class AchievementsMenuState : SceneMenuState 
    {
        public override BibaScene BibaScene {
            get 
            {
                return BibaScene.Achievements;
            }
        }
    }
}