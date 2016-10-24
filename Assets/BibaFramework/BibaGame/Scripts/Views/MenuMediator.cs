using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class MenuMediator : SceneMenuStateMediator
	{
		[Inject]
		public MenuView MenuView { get; set; }
		
		public override SceneMenuStateView View { get { return MenuView; } }
		
		public override void SetupSceneDependentMenu ()
		{
		}
		
		public override void RegisterSceneDependentSignals ()
		{
		}
		
		public override void UnRegisterSceneDependentSignals ()
		{
		}
	}
}