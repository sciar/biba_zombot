using System;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class TagInitFailedCommand : Command
	{
		[Inject]
		public BibaDevice BibaDevice { get; set; }

		[Inject]
		public IDataService DataService { get; set; }

		public override void Execute ()
		{
			BibaDevice.LastCameraReminderTime = DateTime.UtcNow;
			DataService.Save ();
		}
	}
}