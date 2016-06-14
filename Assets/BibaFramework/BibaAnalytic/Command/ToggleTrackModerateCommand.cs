using System;
using strange.extensions.command.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaAnalytic
{
	public class ToggleTrackModerateCommand : Command
	{
		[Inject]
		public bool Status { get; set; }

		[Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

		[Inject]
		public ToggleTrackSedentarySignal ToggleTrackSedentarySignal { get; set; }

		[Inject]
		public ToggleTrackVigorousSignal ToggleTrackVigorousSignal { get; set; }

		public override void Execute ()
		{
			if (Status) 
			{
				ToggleTrackSedentarySignal.Dispatch (false);
				ToggleTrackVigorousSignal.Dispatch (false);

				BibaSessionModel.RoundInfo.ModerateTrackingStartTime = DateTime.UtcNow;
			}
			else 
			{
				if (BibaSessionModel.RoundInfo.ModerateTrackingStartTime != default(DateTime)) 
				{
					BibaSessionModel.RoundInfo.ModerateTime += (float)(DateTime.UtcNow - BibaSessionModel.RoundInfo.ModerateTrackingStartTime).TotalSeconds;
					BibaSessionModel.RoundInfo.ModerateTrackingStartTime = default(DateTime);
				}
			}
		}
	}
}