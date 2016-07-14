using System;
using BibaFramework.BibaAnalytic;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class StartNewSessionCommand : Command
	{
		[Inject]
		public BibaSession BibaSession { get; set; }

		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackLightActivitySignal { get; set; }

		[Inject]
		public IAnalyticService AnalyticService { get; set; }

		public override void Execute ()
		{
			if (BibaSession.SelectedEquipments.Count > 0) 
			{
				BibaSession.End = DateTime.UtcNow;
				AnalyticService.TrackEndSession ();
			}

			//TODO: Call SessionEnd call for tracking

			BibaSession = new BibaSession ();
			AnalyticService.TrackStartSession ();
			ToggleTrackLightActivitySignal.Dispatch (true);
		}
	}
}