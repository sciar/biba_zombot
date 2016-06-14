using System;
using strange.extensions.command.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaAnalytic
{
	public class ToggleTrackSedentaryCommand : Command
	{
		[Inject]
		public bool Status { get; set; }

		[Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

		[Inject]
		public ToggleTrackModerateSignal ToggleTrackModerateSignal { get; set; }

		[Inject]
		public ToggleTrackVigorousSignal ToggleTrackVigorousSignal { get; set; }

		public override void Execute ()
		{
			if (Status) 
			{
				BibaSessionModel.RoundInfo.SedentaryTrackingStartTime = DateTime.UtcNow;

				ToggleTrackModerateSignal.Dispatch (false);
				ToggleTrackVigorousSignal.Dispatch (false);
			}
			else 
			{
				if (BibaSessionModel.RoundInfo.SedentaryTrackingStartTime != default(DateTime)) 
				{
					BibaSessionModel.RoundInfo.SedentaryTime += (float)(DateTime.UtcNow - BibaSessionModel.RoundInfo.SedentaryTrackingStartTime).TotalSeconds;
					BibaSessionModel.RoundInfo.SedentaryTrackingStartTime = default(DateTime);
				}
			}
		}
	}
}