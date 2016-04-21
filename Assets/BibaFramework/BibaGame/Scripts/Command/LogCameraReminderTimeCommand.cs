using strange.extensions.command.impl;
using System;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class LogCameraReminderTimeCommand : Command
    {
        [Inject]
		public BibaSessionModel BibaSessionModel { get; set; }

        public override void Execute ()
        {
			BibaSessionModel.LastCameraReminderTime = DateTime.UtcNow;
        }
    }
}