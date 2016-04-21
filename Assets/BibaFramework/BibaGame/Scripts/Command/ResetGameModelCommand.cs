using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class ResetGameModelCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

		[Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        public override void Execute ()
        {
            BibaGameModel.Reset();
            DataService.WriteGameModel();

			BibaSessionModel.Reset();

            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Reset);
        }
    }
}