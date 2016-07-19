using BibaFramework.BibaMenu;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaGame
{
	public class BibaGameMediator : SceneMenuStateMediator
	{
		[Inject]
		public BibaGameView view { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

		[Inject]
		public BibaDeviceSession BibaSession { get; set; }

		[Inject]
		public IDataService service { get; set; }

		public override void RegisterSceneDependentSignals ()
		{
			view.controller.BibaAccount = BibaAccount;
			view.controller.BibaSession = BibaSession;
			view.controller.DataService = service;
		}

		public override void UnRegisterSceneDependentSignals ()
		{
			service.Save ();
		}

        public override void SetupSceneDependentMenu ()
		{
		}

		public override SceneMenuStateView View {
			get {
				return view;
			}
		}
	}
}