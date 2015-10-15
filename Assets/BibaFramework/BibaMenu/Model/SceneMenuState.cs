using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
	public abstract class SceneMenuState : BaseMenuState 
	{
		public override string SceneName {
			get {
				return GameScene.ToString();
			}
		}
        public abstract BibaScene GameScene { get; }
        public bool Popup;
		public bool FullScreen { get { return !Popup; }  } 
	}
}