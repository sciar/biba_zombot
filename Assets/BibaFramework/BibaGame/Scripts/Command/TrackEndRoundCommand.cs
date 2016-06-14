using strange.extensions.command.impl;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
	public class TrackEndRoundCommand : Command
	{
		[Inject]
		public IAnalyticService AnalyticService { get; set; }

		public override void Execute ()
		{
			AnalyticService.TrackEndRound ();
		}
	}
}