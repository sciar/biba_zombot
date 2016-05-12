using System;
using BibaFramework.BibaNetwork;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class PointEventService : BaseSettingsService<BibaPointEventSettings>
	{
		[Inject]
		public BibaGameModel BibaGameModel { get; set; }

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
			var setting = Settings.BibaPointSettings.Find(sett => sett.Key == keyToCheck);
			if(setting == null)
			{
				return;
			}

			var pointEventCompleted = BibaGameModel.CompletedPointsEvent.Find(pe => pe == keyToCheck);
			if(pointEventCompleted == null || setting.Repeat)
			{
				BibaGameModel.Points += setting.Points;
				BibaGameModel.CompletedPointsEvent.Add(keyToCheck);

				DataService.WriteGameModel();
				PointsGainedSignal.Dispatch (setting.Points, BibaGameModel.Points);
			}
		}
	}
}