using strange.extensions.command.impl;
using BibaFramework.BibaMenu;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
	public class EnableTagCommand : Command
    {
		[Inject]
		public bool Status { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        [Inject]
        public IBibaAnalyticService BibaAnalyticService { get; set; } 

        public override void Execute ()
        {
			BibaGameModel.TagEnabled = Status;
            DataService.WriteGameModel();

            BibaAnalyticService.TrackSatelliteTagEnabled(Status);
			SetMenuStateConditionSignal.Dispatch(MenuStateCondition.TagEnabled, Status);
        }
    }
}