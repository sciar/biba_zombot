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
		public BibaProfile BibaProfile { get; set; }

		[Inject]
		public BibaDevice BibaDevice { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public IDataService service { get; set; }

		public override void RegisterSceneDependentSignals ()
		{
			view.controller.BibaAccount = BibaAccount;
			view.controller.BibaProfile = BibaProfile;
			view.controller.BibaDevice = BibaDevice;
			view.controller.BibaDeviceSession = BibaDeviceSession;
			view.controller.DataService = service;
			view.controller.SetupController ();
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