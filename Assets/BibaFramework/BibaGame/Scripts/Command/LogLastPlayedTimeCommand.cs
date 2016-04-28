using strange.extensions.command.impl;
using System;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class LogLastPlayedTimeCommand : Command
    {
        [Inject]
		public BibaGameModel BibaGameModel { get; set; }
	
		[Inject]
		public IDataService DataService { get; set; }

        public override void Execute ()
        {
			if (BibaGameModel.SelectedEquipments.Count > 0)
            {
				BibaGameModel.LastPlayedTime = DateTime.UtcNow;
				DataService.WriteGameModel ();
            }
        }
    }
}