using strange.extensions.command.impl;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
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