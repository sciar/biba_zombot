using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class InactiveMediator : SceneMenuStateMediator
	{
        [Inject]
        public InactiveView InactiveView { get; set; }

        [Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

		[Inject]
		public ClearEquipmentsSignal ClearEquipmentsSignal { get; set; }

        public override SceneMenuStateView View { get { return InactiveView; } }

        public override void SetupSceneDependentMenu ()
        {
			var lastPlayedTimeLocal = BibaSessionModel.LastPlayedTime.ToLocalTime();
			InactiveView.Text.text = string.Format(InactiveView.Text.text, lastPlayedTimeLocal.ToShortDateString(), lastPlayedTimeLocal.ToShortTimeString());
        }

        public override void RegisterSceneDependentSignals ()
        {
			InactiveView.NoButton.onClick.AddListener (ClearSelectedEquipments);
        }
        
        public override void UnRegisterSceneDependentSignals ()
        {
			InactiveView.NoButton.onClick.RemoveListener (ClearSelectedEquipments);
        }

		void ClearSelectedEquipments()
		{
			ClearEquipmentsSignal.Dispatch ();
		}
	}
}