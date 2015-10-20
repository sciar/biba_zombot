using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class ChartBoostMediator : SceneMenuStateMediator
	{
        [Inject]
        public ChartBoostView ChartBoostView { get; set; }

        public override SceneMenuStateView View { get { return ChartBoostView; } }

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