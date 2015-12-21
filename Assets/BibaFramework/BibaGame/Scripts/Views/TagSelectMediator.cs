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
			TagSelectView.EnableTagSignal.AddListener(TagSelected);
        }

        public override void UnRegisterSceneDependentSignals ()
		{
			TagSelectView.EnableTagSignal.RemoveListener(TagSelected);
        }

        public override void SetupSceneDependentMenu ()	
        {
        }

		void TagSelected(bool status)
		{
			EnableTagSignal.Dispatch(status);
		}
    }
}