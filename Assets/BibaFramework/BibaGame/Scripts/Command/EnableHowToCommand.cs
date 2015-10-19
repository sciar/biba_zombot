using strange.extensions.command.impl;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class EnableHowToCommand : Command
    {
        [Inject]
        public bool Status { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
            BibaGameModel.HowToEnabled = Status;
            DataService.WriteGameModel();

            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HowToEnabled, Status);
        }
    }
}