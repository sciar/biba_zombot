using System;

namespace BibaFramework.BibaGame
{
    public class BibaGameConstants
    {
        public const int FRAMEWORK_VERSION = 2;
		public const string ACCOUNT_MODEL_DATA_PATH = "account.txt";
		public const string SYSTEM_MODEL_DATA_PATH = "system.txt";

        public const string BIBA_URL = "http://www.playbiba.com";

        private const int ONE_DAY_IN_SECONDS = 86400; //1 day
        private const int ONE_HOUR_IN_SECONDS = 3600; //1 hr

        public static readonly TimeSpan AR_REMINDER_DURATION = TimeSpan.FromSeconds(ONE_DAY_IN_SECONDS);
        public static readonly TimeSpan CHARTBOOST_CHECK_DURATION = TimeSpan.FromSeconds(ONE_DAY_IN_SECONDS);
        public static readonly TimeSpan INACTIVE_DURATION = TimeSpan.FromSeconds(ONE_HOUR_IN_SECONDS);

        //Achievement
        public const string ACHIEVEMENT_PREFIX_KEY_BRIDGE = "prefix_bridge";
        public const string ACHIEVEMENT_PREFIX_KEY_CLIMBER = "prefix_climber";
        public const string ACHIEVEMENT_PREFIX_KEY_TUBE = "prefix_tube";
        public const string ACHIEVEMENT_PREFIX_KEY_SLIDE = "prefix_slide";
        public const string ACHIEVEMENT_PREFIX_KEY_SWING = "prefix_swing";
        public const string ACHIEVEMENT_PREFIX_KEY_OVERHANG = "prefix_overhang";
    }
}