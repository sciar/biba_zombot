using System;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaGameConstants
    {
        public const int FRAMEWORK_VERSION = 2;
		public const string ACCOUNT_MODEL_DATA_PATH = "account.txt";
		public const string DEVICE_MODEL_DATA_PATH = "device.txt";

        public const string BIBA_URL = "http://www.playbiba.com";

        private const int ONE_DAY_IN_SECONDS = 86400; //1 day
        private const int ONE_HOUR_IN_SECONDS = 3600; //1 hr

        public static readonly TimeSpan CHARTBOOST_CHECK_DURATION = TimeSpan.FromSeconds(ONE_DAY_IN_SECONDS);
        public static readonly TimeSpan INACTIVE_DURATION = TimeSpan.FromSeconds(ONE_HOUR_IN_SECONDS);

        //Achievement
        public const string ACHIEVEMENT_PREFIX_KEY_BRIDGE = "prefix_bridge";
        public const string ACHIEVEMENT_PREFIX_KEY_CLIMBER = "prefix_climber";
        public const string ACHIEVEMENT_PREFIX_KEY_TUBE = "prefix_tube";
        public const string ACHIEVEMENT_PREFIX_KEY_SLIDE = "prefix_slide";
        public const string ACHIEVEMENT_PREFIX_KEY_SWING = "prefix_swing";
        public const string ACHIEVEMENT_PREFIX_KEY_OVERHANG = "prefix_overhang";

		public static List<BibaEquipment> DEFAULT_EQUIPENT_LIST 
		{
			get 
			{
				var defaults = new List<BibaEquipment> ();
				Array.ForEach((BibaEquipmentType[])Enum.GetValues(typeof(BibaEquipmentType)), eq => defaults.Add(new BibaEquipment(eq)));
				return defaults;
			}
		}

		//Tag
		public static readonly float TAG_FLOAT_TO_SCREEN_TIME = Time.deltaTime * 4f;
		public static readonly TimeSpan AR_REMINDER_DURATION = TimeSpan.FromSeconds(ONE_DAY_IN_SECONDS);
    }
}