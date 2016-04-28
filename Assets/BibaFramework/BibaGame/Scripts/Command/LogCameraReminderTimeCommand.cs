using strange.extensions.command.impl;
using System;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class LogCameraReminderTimeCommand : Command
    {
        [Inject]
		public BibaGameModel BibaGameModel { get; set; }

		[Inject]
		public IDataService DataService { get; set; }

        public override void Execute ()
        {
			BibaGameModel.LastCameraReminderTime = DateTime.UtcNow;
			DataService.WriteGameModel ();
        }
    }
}