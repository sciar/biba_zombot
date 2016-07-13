using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class ResetGameModelCommand : Command
    {
        [Inject]
		public BibaSystem BibaSystem { get; set; }

		[Inject]
		public BibaSession BibaSession { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

		[Inject]
		public LanguageUpdatedSignal LanguageUpdatedSignal { get; set; }

        public override void Execute ()
        {
			BibaAccount = new BibaAccount ();
			BibaSystem = new BibaSystem ();
			BibaSession = new BibaSession ();
			DataService.Save ();

			LanguageUpdatedSignal.Dispatch ();
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Reset);
        }
    }
}