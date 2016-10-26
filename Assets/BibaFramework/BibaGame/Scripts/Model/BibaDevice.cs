using UnityEngine;
using System;
using System.Collections.Generic;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
	[Serializable]
	public class BibaDevice : IResetModel
	{
		public string Id { get { return BibaContentConstants.CI_GAME_ID; } }
		public string DeviceId { get { return SystemInfo.deviceUniqueIdentifier; } }
		public string InstanceId;

		public int Highscore;
		public int FrameworkVersion;

		public bool PrivacyEnabled;
		public bool HowToEnabled;
		public bool HelpBubblesEnabled;
		public SystemLanguage LanguageOverwrite;

		public DateTime LastPlayedTime;
		public DateTime LastChartBoostTime;
		public DateTime LastCameraReminderTime;

		public List<BibaAchievement> CompletedAchievements;
		public List<BibaEquipment> TotalEquipments;

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