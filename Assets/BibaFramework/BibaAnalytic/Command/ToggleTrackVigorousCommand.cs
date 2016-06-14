using System;
using strange.extensions.command.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaAnalytic
{
	public class ToggleTrackVigorousCommand : Command
	{
		[Inject]
		public bool Status { get; set; }

		[Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

		[Inject]
		public ToggleTrackSedentarySignal ToggleTrackSedentarySignal { get; set; }

		[Inject]
		public ToggleTrackModerateSignal ToggleTrackModerateSignal { get; set; }

		public override void Execute ()
		{
			if (Status) 
			{
				BibaSessionModel.RoundInfo.VigorousTrackingStartTime = DateTime.UtcNow;

				ToggleTrackSedentarySignal.Dispatch (false);
				ToggleTrackModerateSignal.Dispatch (false);
			}
			else 
			{
				if (BibaSessionModel.RoundInfo.VigorousTrackingStartTime != default(DateTime)) 
				{
					BibaSessionModel.RoundInfo.VigorousTime += (float)(DateTime.UtcNow - BibaSessionModel.RoundInfo.VigorousTrackingStartTime).TotalSeconds;
					BibaSessionModel.RoundInfo.VigorousTrackingStartTime = default(DateTime);
				}
			}
		}
	}
}