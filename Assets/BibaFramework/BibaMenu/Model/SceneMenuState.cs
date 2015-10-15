using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
	public abstract class SceneMenuState : BaseMenuState 
	{
        public override bool FullScreen { get { return !Popup; } }
        public bool Popup;

		public override string SceneName {
			get {
				return BibaScene.ToString();
			}
		}
        public abstract BibaScene BibaScene { get; }
	}
}