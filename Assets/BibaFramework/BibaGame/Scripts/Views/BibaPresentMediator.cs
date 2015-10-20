using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class BibaPresentMediator : SceneMenuStateMediator
	{
        [Inject]
        public BibaPresentView BibaPresentView { get; set; }

        public override SceneMenuStateView View { get { return BibaPresentView; } }

        public override void SetupMenu (BaseMenuState menuState)
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