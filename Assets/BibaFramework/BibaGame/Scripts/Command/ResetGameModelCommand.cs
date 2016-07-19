using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class ResetGameModelCommand : Command
    {
        [Inject]
		public BibaDevice BibaDevice { get; set; }

		[Inject]
		public BibaDeviceSession BibaSession { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

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
			BibaSession = new BibaDeviceSession ();
			DataService.Save ();

			LanguageUpdatedSignal.Dispatch ();
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Reset);
        }
    }
}