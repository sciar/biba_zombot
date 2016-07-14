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

		public override void Execute ()
		{
			BibaSession.End = DateTime.UtcNow;

			//TODO: Call SessionEnd call for tracking

			BibaSession = new BibaSession ();
			ToggleTrackLightActivitySignal.Dispatch (true);
		}
	}
}