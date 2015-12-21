using BibaFramework.BibaMenu;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaGame
{
	public class BibaGameMediator : SceneMenuStateMediator
	{
		[Inject]
		public BibaGameView view { get; set; }

		[Inject]
		public BibaGameModel model { get; set; }

		[Inject]
		public IDataService service { get; set; }

		public override void RegisterSceneDependentSignals ()
		{
			view.controller.model = model;
			view.controller.dataService = service;
		}

		public override void UnRegisterSceneDependentSignals ()
		{
			service.WriteGameModel ();
		}

        public override void SetupSceneDependentMenu ()
		{
			//throw new System.NotImplementedException ();
		}

		public override SceneMenuStateView View {
			get {
				return view;
			}
		}
	}
}