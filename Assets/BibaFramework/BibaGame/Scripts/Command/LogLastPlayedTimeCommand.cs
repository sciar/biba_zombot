using strange.extensions.command.impl;
using System;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class LogLastPlayedTimeCommand : Command
    {
        [Inject]
		public BibaSystem BibaSystem { get; set; }
	
		[Inject]
		public IDataService DataService { get; set; }

        public override void Execute ()
        {
			BibaSystem.LastPlayedTime = DateTime.UtcNow;
			DataService.Save ();
        }
    }
}