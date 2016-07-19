using UnityEngine;
using System;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	public class BibaDevice
	{
		public string UUID { get; set; }

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
		public List<BibaEquipment> PlayedEquipments { get; set; }

		public BibaDevice()
		{
			UUID = Guid.NewGuid().ToString();

			CompletedAchievements = new List<BibaAchievement> ();
			LanguageOverwrite = SystemLanguage.Unknown;
			LastPlayedTime = DateTime.MaxValue;
			PlayedEquipments = BibaGameConstants.DEFAULT_EQUIPENT_LIST;
		}
	}
}