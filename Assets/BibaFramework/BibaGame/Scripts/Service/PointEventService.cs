using System;
using BibaFramework.BibaNetwork;
using System.Linq;

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

			if(!BibaProfile.CompletedPointEvents.Contains(keyToCheck) || setting.Repeat)
			{
				BibaProfile.Points += setting.Points;

				if (!BibaProfile.CompletedPointEvents.Contains(keyToCheck)) 
				{
					BibaProfile.CompletedPointEvents.Add(keyToCheck);
				}

				DataService.Save();
				PointsGainedSignal.Dispatch (setting.Points, BibaProfile.Points);
			}

			CheckForLMVPointEvents();
		}

		void CheckForLMVPointEvents()
		{
			var pointsGained = 0;
			var lmvSettings = Settings.BibaPointSettings.Where(setting => setting is BibaLMVPointEvent);
			foreach (BibaLMVPointEvent setting in lmvSettings) 
			{
				if (!BibaProfile.BibaProfileSession.CompletedLMVScoreEvents.Contains (setting.Id) || setting.Repeat) 
				{
					if (BibaProfile.BibaProfileSession.LMVSessionDict [setting.LMVScoreType].SessionScore >= setting.ScoreRequired) 
					{
						BibaProfile.Points += setting.Points;
						if (!BibaProfile.CompletedPointEvents.Contains(setting.Id)) 
						{
							BibaProfile.CompletedPointEvents.Add(setting.Id);
						}
					}
				}
			}

			if (pointsGained > 0) 
			{
				DataService.Save();
				PointsGainedSignal.Dispatch (pointsGained, BibaProfile.Points);
			}
		}
	}
}