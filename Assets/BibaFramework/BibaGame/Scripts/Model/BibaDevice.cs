using UnityEngine;
using System;
using System.Collections.Generic;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
	public class BibaDevice : IResetModel
	{
		public string Id { get { return BibaContentConstants.CI_GAME_ID; } }
		public string DeviceId { get { return SystemInfo.deviceUniqueIdentifier; } }
		public string InstanceId { get; set; }

		public int Highscore { get; set; }
		public int FrameworkVersion { get; set; }

		public bool PrivacyEnabled { get; set; }
		public bool HowToEnabled { get; set; }
		public bool HelpBubblesEnabled { get; set; }
		public SystemLanguage LanguageOverwrite { get; set; }

		public DateTime LastPlayedTime { get; set; }
		public DateTime LastChartBoostTime { get; set; }
		public DateTime LastCameraReminderTime { get; set; }

		public List<BibaAchievement> CompletedAchievements { get; set; }
		public List<BibaEquipment> TotalEquipments { get; set; }

		public BibaDevice()
		{
			Reset ();
		}

		public void Reset()
		{
			InstanceId = Guid.NewGuid().ToString();

			Highscore = 0;

			PrivacyEnabled = false;
			HowToEnabled = true;
			HelpBubblesEnabled = true;
			LanguageOverwrite = SystemLanguage.Unknown;

			LastPlayedTime = DateTime.MaxValue;
			LastChartBoostTime = default(DateTime);
			LastCameraReminderTime = default(DateTime);

			CompletedAchievements = new List<BibaAchievement> ();
			TotalEquipments = BibaGameConstants.DEFAULT_EQUIPENT_LIST;
		}


	}
}