using System;
using BibaFramework.BibaAnalytic;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class StartNewSessionCommand : Command
	{
		[Inject]
		public BibaDeviceSession BibaGameSession { get; set; }

		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackLightActivitySignal { get; set; }

		[Inject]
		public IAnalyticService AnalyticService { get; set; }

		public override void Execute ()
		{
			if (BibaGameSession.SelectedEquipments.Count > 0) 
			{
				BibaGameSession.End = DateTime.UtcNow;
				AnalyticService.TrackEndSession ();
			}
				
			BibaGameSession = new BibaDeviceSession ();
			AnalyticService.TrackStartSession ();
			ToggleTrackLightActivitySignal.Dispatch (true);
		}
	}
}