using strange.extensions.command.impl;
using System;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class LogLastPlayedTimeCommand : Command
    {
        [Inject]
		public BibaSessionModel BibaSessionModel { get; set; }
	
        public override void Execute ()
        {
			if (BibaSessionModel.SelectedEquipments.Count > 0)
            {
				BibaSessionModel.LastPlayedTime = DateTime.UtcNow;
            }
        }
    }
}