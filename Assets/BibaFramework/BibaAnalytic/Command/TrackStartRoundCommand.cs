using strange.extensions.command.impl;

namespace BibaFramework.BibaAnalytic
{
	public class TrackStartRoundCommand : Command
	{
		[Inject]
		public IAnalyticService AnalyticService { get; set; }

		public override void Execute ()
		{
			AnalyticService.TrackStartRound ();
		}
	}
}