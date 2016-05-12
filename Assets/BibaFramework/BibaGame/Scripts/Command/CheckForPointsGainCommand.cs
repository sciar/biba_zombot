using System;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class CheckForPointsGainCommand : Command
	{
		[Inject]
		public string keyToCheck { get; set; }

		[Inject]
		public PointEventService PointEventService { get; set; }

		public override void Execute ()
		{
			PointEventService.CheckAndCompletePointEvent (keyToCheck);
		}
	}
}