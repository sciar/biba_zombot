using BibaFramework.BibaMenu;
using System.Linq;

namespace BibaFramework.BibaGame
{
	public class TagSelectMediator : SceneMenuStateMediator
    {
        [Inject]
		public TagSelectView TagSelectView { get; set; }

        [Inject]
		public EnableTagSignal EnableTagSignal { get; set; }

        public override SceneMenuStateView View {
            get {
                return TagSelectView;
            }
        }

        public override void RegisterSceneDependentSignals ()
        {
			TagSelectView.EnableTagSignal.AddListener(TagEnabled);
        }

        public override void UnRegisterSceneDependentSignals ()
		{
			TagSelectView.EnableTagSignal.RemoveListener(TagEnabled);
        }

        public override void SetupSceneDependentMenu ()	
        {
        }

		void TagEnabled(bool status)
		{
			EnableTagSignal.Dispatch(status);
		}
    }
}