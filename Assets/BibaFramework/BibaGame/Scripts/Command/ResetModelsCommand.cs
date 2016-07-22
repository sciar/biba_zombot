using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class ResetModelsCommand : Command
    {
        [Inject]
		public BibaDevice BibaDevice { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

		[Inject]
		public BibaProfile BibaProfile { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

		[Inject]
		public SystemUpdatedSignal LanguageUpdatedSignal { get; set; }

        public override void Execute ()
        {
			BibaAccount = new BibaAccount ();
			BibaDevice = new BibaDevice ();
			DataService.Save ();

			BibaProfile = BibaAccount.BibaProfiles [0];
			BibaDeviceSession = new BibaDeviceSession ();

			LanguageUpdatedSignal.Dispatch ();
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Reset);
        }
    }
}