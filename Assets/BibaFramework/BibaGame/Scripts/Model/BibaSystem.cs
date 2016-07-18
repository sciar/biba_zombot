using UnityEngine;
using System;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	public class BibaSystem
	{
		public string UUID { get; set; }
		public string DeviceModel { get; set; }
		public string DeviceOS { get; set; }

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

		public BibaSystem()
		{
			UUID = Guid.NewGuid().ToString();
			DeviceModel = SystemInfo.deviceModel;
			DeviceOS = SystemInfo.operatingSystem;

			CompletedAchievements = new List<BibaAchievement> ();
			LanguageOverwrite = SystemLanguage.Unknown;
			LastPlayedTime = DateTime.MaxValue;
			PlayedEquipments = DEFAULT_EQUIPENT_LIST;
		}

		private static List<BibaEquipment> DEFAULT_EQUIPENT_LIST 
		{
			get 
			{
				var defaults = new List<BibaEquipment> ();
				Array.ForEach((BibaEquipmentType[])Enum.GetValues(typeof(BibaEquipmentType)), eq => defaults.Add(new BibaEquipment(eq)));
				return defaults;
			}
		}
	}
}