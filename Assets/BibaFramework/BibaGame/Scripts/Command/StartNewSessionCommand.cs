using System;
using BibaFramework.BibaAnalytic;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class StartNewSessionCommand : Command
	{
		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

		[Inject]
		public ToggleTrackLightActivitySignal ToggleTrackLightActivitySignal { get; set; }

		[Inject]
		public IDeviceAnalyticService AnalyticService { get; set; }

		public override void Execute ()
		{
			if (BibaDeviceSession.SelectedEquipments.Count > 0) 
			{
				AnalyticService.TrackEndSession ();
			}
				
			BibaDeviceSession = new BibaDeviceSession ();
			foreach (var profile in BibaAccount.BibaProfiles) 
			{
				profile.BibaProfileSession = new BibaProfileSession ();
			}

			AnalyticService.TrackStartSession ();
			ToggleTrackLightActivitySignal.Dispatch (true);
		}
	}
}