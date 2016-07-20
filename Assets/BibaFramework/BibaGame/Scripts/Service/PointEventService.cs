using System;
using BibaFramework.BibaNetwork;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class PointEventService : BaseSettingsService<BibaPointEventSettings>
	{
		[Inject]
		public BibaProfile BibaProfile { get; set; }

		[Inject]
		public PointsGainedSignal PointsGainedSignal { get; set; }

		public override string SettingsFileName {
			get {
				return BibaContentConstants.POINTEVENT_SETTINGS_FILE;
			}
		}

		#region - IContentUpdated
		public override void ReloadContent()
		{
			_settings = DataService.ReadFromDisk<BibaPointEventSettings>(ContentFilePath);
		}
		#endregion

		public void CheckAndCompletePointEvent(string keyToCheck)
		{
			var setting = Settings.BibaPointSettings.Find(sett => sett.Id == keyToCheck);
			if(setting == null)
			{
				return;
			}

			var pointEventCompleted = BibaProfile.CompletedPointEvents.Find(pe => pe == keyToCheck);
			if(pointEventCompleted == null || setting.Repeat)
			{
				BibaProfile.Points += setting.Points;

				if (!BibaProfile.CompletedPointEvents.Contains(keyToCheck)) 
				{
					BibaProfile.CompletedPointEvents.Add(keyToCheck);
				}

				DataService.Save();
				PointsGainedSignal.Dispatch (setting.Points, BibaProfile.Points);
			}
		}
	}
}