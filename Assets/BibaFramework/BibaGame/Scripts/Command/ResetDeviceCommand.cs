using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class ResetDeviceCommand : Command
    {
        [Inject]
		public BibaDevice BibaDevice { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

		[Inject]
		public SystemUpdatedSignal LanguageUpdatedSignal { get; set; }

        public override void Execute ()
        {
			BibaDevice.Reset();
			DataService.Save ();

			BibaDeviceSession.Reset();

			LanguageUpdatedSignal.Dispatch ();
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Reset);
        }
    }
}