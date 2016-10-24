using strange.extensions.command.impl;
using System;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class LogLastPlayedTimeCommand : Command
    {
        [Inject]
		public BibaDevice BibaDevice { get; set; }
	
		[Inject]
		public IDataService DataService { get; set; }

        public override void Execute ()
        {
			BibaDevice.LastPlayedTime = DateTime.UtcNow;
			DataService.Save ();
        }
    }
}